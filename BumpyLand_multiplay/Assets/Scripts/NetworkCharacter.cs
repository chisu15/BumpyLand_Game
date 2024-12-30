using ActiveRagdoll;
using Fusion;
using UnityEngine;

public class NetworkCharacter : NetworkBehaviour
{
    [Networked] public Vector3 NetworkedPosition { get; set; }
    [Networked] public Quaternion NetworkedRotation { get; set; }
    [Networked] public int NetworkedAnimState { get; set; }

    [SerializeField] private Transform _characterTransform;
    private CharacterMovement _characterMovement;
    [SerializeField] private Animator _animator;
    [SerializeField] private PhysicsModule _physicsModule;
    [SerializeField] private AnimationModule _animationModule;
    [SerializeField] private int damageAmount = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (!HasStateAuthority) return;

        
        if (collision.gameObject.TryGetComponent<HealthSystem>(out var healthSystem))
        {
            healthSystem.RPC_TakeDamage(damageAmount);
        }
    }
    public override void Spawned()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        _animator = GetComponentInChildren<Animator>();
        _physicsModule = GetComponent<PhysicsModule>();
        _animationModule = GetComponent<AnimationModule>();
    }

    public override void FixedUpdateNetwork()
    {
        if (HasInputAuthority)
        {
            Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            bool isJumpPressed = Input.GetButton("Jump");
            Vector3 aimDirection = Camera.main.transform.forward;

            RPC_SendInput(moveDirection, isJumpPressed, aimDirection);
        }

        if (!HasStateAuthority)
        {
            _characterTransform.position = Vector3.MoveTowards(_characterTransform.position, NetworkedPosition, Runner.DeltaTime * 10);
            _characterTransform.rotation = Quaternion.RotateTowards(_characterTransform.rotation, NetworkedRotation, Runner.DeltaTime * 500);

            if (_animator != null)
            {
                _animator.SetInteger("animState", NetworkedAnimState);
            }
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SendInput(Vector2 moveDirection, bool isJumpPressed, Vector3 aimDirection)
    {
        if (moveDirection.magnitude > 0.1f)
        {
            Vector3 direction = aimDirection.normalized;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _characterTransform.rotation = Quaternion.Slerp(_characterTransform.rotation, targetRotation, Runner.DeltaTime * 10);

            _characterTransform.position += new Vector3(moveDirection.x, 0, moveDirection.y) * Runner.DeltaTime * 5;

            _animationModule.Animator.SetBool("moving", true);
            _animationModule.Animator.SetFloat("speed", moveDirection.magnitude);

            NetworkedAnimState = 1;
        }
        else
        {
            _animationModule.Animator.SetBool("moving", false);
            NetworkedAnimState = 0;
        }

        NetworkedPosition = _characterTransform.position;
        NetworkedRotation = _characterTransform.rotation;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_SynchronizeState()
    {
        if (HasStateAuthority)
        {
            NetworkedPosition = _characterTransform.position;
            NetworkedRotation = _characterTransform.rotation;

            if (_animationModule.Animator.GetBool("moving"))
            {
                NetworkedAnimState = 1;
            }
            else
            {
                NetworkedAnimState = 0;
            }
        }
    }
    
}
