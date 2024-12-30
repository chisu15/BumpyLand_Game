using UnityEngine;
using ActiveRagdoll;
using Fusion;

public class CharacterMovement : NetworkBehaviour
{
    [Header("Modules")]
    [SerializeField] private ActiveRagdoll.ActiveRagdoll _activeRagdoll;
     [SerializeField] private PhysicsModule _physicsModule;
    [SerializeField] private AnimationModule _animationModule;


    [Header("Movement")]
    [SerializeField] private bool _enableMovement = true;
    private Vector2 _movement;
     private Vector3 _aimDirection;

   [Networked]public bool IsMoving { get; private set; } = false;

    private void OnValidate()
    {
          if (_activeRagdoll == null) _activeRagdoll = GetComponent<ActiveRagdoll.ActiveRagdoll>();
          if (_physicsModule == null) _physicsModule = GetComponent<PhysicsModule>();
         if (_animationModule == null) _animationModule = GetComponent<AnimationModule>();
    }
    
    public void SetAimDirection(Vector3 aimDirection){
          _aimDirection = aimDirection;
    }
    public void Move(Vector2 movement, bool isJumpPressed)
    {
        _movement = movement;
        UpdateMovement();
         

        
       if(isJumpPressed){
             Debug.Log("Jumped");
        }
    }

      public Vector2 GetMoveDirection()
    {
        return _movement;
    }
    public bool IsJumpPressed()
    {
        
       return Input.GetButton("Jump");
    }

    private float movementThreshold = 0.1f;

    private void UpdateMovement()
    {
     if (_movement.magnitude < movementThreshold || !_enableMovement)
     {
      _animationModule.Animator.SetBool("moving", false);
      IsMoving = false;
      return;
     }

     _animationModule.Animator.SetBool("moving", true);
     _animationModule.Animator.SetFloat("speed", _movement.magnitude);
     IsMoving = true;

     float angleOffset = Vector2.SignedAngle(_movement, Vector2.up);
     Vector3 targetForward = Quaternion.AngleAxis(angleOffset, Vector3.up) * Auxiliary.GetFloorProjection(_aimDirection);
     _physicsModule.TargetDirection = targetForward;
    }



     public void ProcessFloorChanged(bool onFloor)
   {
       if (onFloor)
        {
            _physicsModule.SetBalanceMode(PhysicsModule.BALANCE_MODE.STABILIZER_JOINT);
             _enableMovement = true;
            _activeRagdoll.GetBodyPart("Head Neck")?.SetStrengthScale(1);
            _activeRagdoll.GetBodyPart("Right Leg")?.SetStrengthScale(1);
           _activeRagdoll.GetBodyPart("Left Leg")?.SetStrengthScale(1);
           _animationModule.PlayAnimation("Idle");
        }
        else
        {
            _physicsModule.SetBalanceMode(PhysicsModule.BALANCE_MODE.MANUAL_TORQUE);
            _enableMovement = false;
             _activeRagdoll.GetBodyPart("Head Neck")?.SetStrengthScale(0.1f);
             _activeRagdoll.GetBodyPart("Right Leg")?.SetStrengthScale(0.05f);
            _activeRagdoll.GetBodyPart("Left Leg")?.SetStrengthScale(0.05f);
             _animationModule.PlayAnimation("InTheAir");
      }
  }
}