using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEditor;
using UnityEngine;

namespace ActiveRagdoll {

    [RequireComponent(typeof(ActiveRagdoll))]
    public class Module : NetworkBehaviour {
        [SerializeField] protected ActiveRagdoll _activeRagdoll;
        public ActiveRagdoll ActiveRagdoll { get { return _activeRagdoll; } }

        private void OnValidate() {
            if (_activeRagdoll == null) {
                if (!TryGetComponent<ActiveRagdoll>(out _activeRagdoll))
                    Debug.LogWarning("No ActiveRagdoll could be found for this module.");
            }
        }
    }
} 
