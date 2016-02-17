﻿using UnityEngine;

namespace UnityCommonLibrary.Colliders {
    [RequireComponent(typeof(Collider))]
    public class ColliderEvents : MonoBehaviour {
        public delegate void OnCollisionEvent(ColliderEvents source, Collision collision);
        public delegate void OnTriggerEvent(ColliderEvents source, Collider other);

        public event OnCollisionEvent CollisionEnter;
        public event OnCollisionEvent CollisionExit;
        public event OnCollisionEvent CollisionStay;

        public event OnTriggerEvent TriggerEnter;
        public event OnTriggerEvent TriggerExit;
        public event OnTriggerEvent TriggerStay;

        public Collider eventCollider { get; private set; }

        private void Awake() {
            eventCollider = GetComponent<Collider>();
        }

        private void OnCollisionEnter(Collision collision) {
            if(CollisionEnter != null) {
                CollisionEnter(this, collision);
            }
        }

        private void OnCollisionExit(Collision collision) {
            if(CollisionExit != null) {
                CollisionExit(this, collision);
            }
        }

        private void OnCollisionStay(Collision collision) {
            if(CollisionStay != null) {
                CollisionStay(this, collision);
            }
        }

        private void OnTriggerEnter(Collider other) {
            if(TriggerEnter != null) {
                TriggerEnter(this, other);
            }
        }

        private void OnTriggerExit(Collider other) {
            if(TriggerExit != null) {
                TriggerExit(this, other);
            }
        }

        private void OnTriggerStay(Collider other) {
            if(TriggerStay != null) {
                TriggerStay(this, other);
            }
        }
    }
}