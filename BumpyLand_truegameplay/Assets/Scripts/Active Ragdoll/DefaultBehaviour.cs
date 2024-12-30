using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActiveRagdoll;
using UnityEngine.SceneManagement; // Import để chuyển scene

/// <summary> Default behaviour of an Active Ragdoll </summary>
public class DefaultBehaviour : MonoBehaviour {

    [Header("Modules")]
    [SerializeField] private ActiveRagdoll.ActiveRagdoll _activeRagdoll;
    [SerializeField] private PhysicsModule _physicsModule;
    [SerializeField] private AnimationModule _animationModule;
    [SerializeField] private GripModule _gripModule;
    [SerializeField] private CameraModule _cameraModule;

    [Header("Movement")]
    [SerializeField] private bool _enableMovement = true;
    [SerializeField] private float jumpForce = 50f;
    private Vector2 _movement;

    private Vector3 _aimDirection;
    private bool _isGrounded = true;

    private void OnValidate() {
        if (_activeRagdoll == null) _activeRagdoll = GetComponent<ActiveRagdoll.ActiveRagdoll>();
        if (_physicsModule == null) _physicsModule = GetComponent<PhysicsModule>();
        if (_animationModule == null) _animationModule = GetComponent<AnimationModule>();
        if (_gripModule == null) _gripModule = GetComponent<GripModule>();
        if (_cameraModule == null) _cameraModule = GetComponent<CameraModule>();
    }
    public AudioSource audio;


    private void CheckMusicOn()
    {
        bool isMusicOff = PlayerPrefs.GetInt("Music", 1) == 0;

        if (isMusicOff)
        {
            if (audio.isPlaying)
            {
                audio.Stop();
            }
        }
        else
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
    }

    void Start() {
        // Link all the functions to its input to define how the ActiveRagdoll will behave.
        CheckMusicOn();
        _activeRagdoll.Input.OnMoveDelegates += MovementInput;
        _activeRagdoll.Input.OnMoveDelegates += _physicsModule.ManualTorqueInput;
        _activeRagdoll.Input.OnFloorChangedDelegates += ProcessFloorChanged;

        _activeRagdoll.Input.OnLeftArmDelegates += _animationModule.UseLeftArm;
        _activeRagdoll.Input.OnLeftArmDelegates += _gripModule.UseLeftGrip;
        _activeRagdoll.Input.OnRightArmDelegates += _animationModule.UseRightArm;
        _activeRagdoll.Input.OnRightArmDelegates += _gripModule.UseRightGrip;
    }

    void Update() {
        Cursor.lockState = CursorLockMode.Locked;
        CheckMusicOn();
        _aimDirection = _cameraModule.Camera.transform.forward;
        _animationModule.AimDirection = _aimDirection;

        UpdateMovement();
        HandleJumping(); 
        

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
#endif
    }

    private void UpdateMovement() {
        if (_movement == Vector2.zero || !_enableMovement) {
            _animationModule.Animator.SetBool("moving", false);
            return;
        }

        _animationModule.Animator.SetBool("moving", true);
        _animationModule.Animator.SetFloat("speed", _movement.magnitude);        

        float angleOffset = Vector2.SignedAngle(_movement, Vector2.up);
        Vector3 targetForward = Quaternion.AngleAxis(angleOffset, Vector3.up) * Auxiliary.GetFloorProjection(_aimDirection);
        _physicsModule.TargetDirection = targetForward;
    }

    private void HandleJumping() {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded) {
            Jump();
        }
    }

    private void Jump() {
        Transform pelvis = _activeRagdoll.GetPhysicalBone(HumanBodyBones.Hips);
        if (pelvis != null && _isGrounded) {
            Rigidbody pelvisRb = pelvis.GetComponent<Rigidbody>();
            if (pelvisRb != null) {
                _physicsModule.PrepareForJump();

                float adjustedJumpForce = jumpForce * 2f;
                pelvisRb.AddForce(Vector3.up * adjustedJumpForce, ForceMode.Impulse);

                _physicsModule.SetBalanceMode(PhysicsModule.BALANCE_MODE.NONE);
                _isGrounded = false;

                _animationModule.PlayAnimation("InTheAir");
                Debug.Log("Jump force applied: " + adjustedJumpForce);
            }
        }
        Debug.Log($"Applying jump force: {jumpForce}");
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.contacts.Length > 0) {
            foreach (var contact in collision.contacts) {
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.6f) { 
                    _isGrounded = true;

                    _physicsModule.ResetAfterJump(); 
                    _physicsModule.SetBalanceMode(PhysicsModule.BALANCE_MODE.STABILIZER_JOINT);
                    _animationModule.PlayAnimation("Idle");

                    Debug.Log("Landed: Constraints reset.");
                    break;
                }
            }
        }
    }


    private void ProcessFloorChanged(bool onFloor) {
        if (onFloor) {
            _physicsModule.SetBalanceMode(PhysicsModule.BALANCE_MODE.STABILIZER_JOINT);
            _enableMovement = true;
            _activeRagdoll.GetBodyPart("Head Neck")?.SetStrengthScale(1);
            _activeRagdoll.GetBodyPart("Right Leg")?.SetStrengthScale(1);
            _activeRagdoll.GetBodyPart("Left Leg")?.SetStrengthScale(1);
            _animationModule.PlayAnimation("Idle");
        }
        else {
            _physicsModule.SetBalanceMode(PhysicsModule.BALANCE_MODE.MANUAL_TORQUE);
            _enableMovement = false;
            _activeRagdoll.GetBodyPart("Head Neck")?.SetStrengthScale(0.1f);
            _activeRagdoll.GetBodyPart("Right Leg")?.SetStrengthScale(0.05f);
            _activeRagdoll.GetBodyPart("Left Leg")?.SetStrengthScale(0.05f);
            _animationModule.PlayAnimation("InTheAir");
        }
    }


    /// <summary> Make the player move and rotate </summary>
    private void MovementInput(Vector2 movement) {
        _movement = movement;
    }
}
