using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActiveRagdoll {

    public class InputModule : Module {
       

        public delegate void onMoveDelegate(Vector2 movement);
        public onMoveDelegate OnMoveDelegates { get; set; }
        public void OnMove(InputValue value) {
            OnMoveDelegates?.Invoke(value.Get<Vector2>());
        }

        public delegate void onLeftArmDelegate(float armWeight);
        public onLeftArmDelegate OnLeftArmDelegates { get; set; }
        public void OnLeftArm(InputValue value) {
            OnLeftArmDelegates?.Invoke(value.Get<float>());
        }

        public delegate void onRightArmDelegate(float armWeight);
        public onRightArmDelegate OnRightArmDelegates { get; set; }
        public void OnRightArm(InputValue value) {
            OnRightArmDelegates?.Invoke(value.Get<float>());
        }

       

        [Header("--- FLOOR ---")]
        public float floorDetectionDistance = 0.3f;
        public float maxFloorSlope = 60;

        private bool _isOnFloor = true;
        public bool IsOnFloor { get { return _isOnFloor; } }

        Rigidbody _rightFoot, _leftFoot;


        void Start() {
            _rightFoot = _activeRagdoll.GetPhysicalBone(HumanBodyBones.RightFoot).GetComponent<Rigidbody>();
            _leftFoot = _activeRagdoll.GetPhysicalBone(HumanBodyBones.LeftFoot).GetComponent<Rigidbody>();
        }

        void Update() {
            UpdateOnFloor();
        }

        public delegate void onFloorChangedDelegate(bool onFloor);
        public onFloorChangedDelegate OnFloorChangedDelegates { get; set; }
        private void UpdateOnFloor() {
            bool lastIsOnFloor = _isOnFloor;

            _isOnFloor = CheckRigidbodyOnFloor(_rightFoot, out Vector3 foo)
                         || CheckRigidbodyOnFloor(_leftFoot, out foo);

            if (_isOnFloor != lastIsOnFloor)
                OnFloorChangedDelegates(_isOnFloor);
        }

        public bool CheckRigidbodyOnFloor(Rigidbody bodyPart, out Vector3 normal) {
           
            Ray ray = new Ray(bodyPart.position, Vector3.down);
            bool onFloor = Physics.Raycast(ray, out RaycastHit info, floorDetectionDistance, ~(1 << bodyPart.gameObject.layer));

           
            onFloor = onFloor && Vector3.Angle(info.normal, Vector3.up) <= maxFloorSlope;

            if (onFloor && info.collider.gameObject.TryGetComponent<Floor>(out Floor floor))
                    onFloor = floor.isFloor;

            normal = info.normal;
            return onFloor;
        }
    }
}