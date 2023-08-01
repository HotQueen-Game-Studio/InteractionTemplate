using UnityEngine;
using UnityEngine.Events;

namespace HotQueen.Interaction
{

    public class Interactor : MonoBehaviour
    {
        //Callbacks
        public UnityEvent<InteractionArg> InteractEnter;
        public UnityEvent<InteractionArg> InteractionCancelled;
        public UnityEvent<ActivateArg> ActivateEnter;
        public UnityEvent<ActivateArg> ActivateCancelled;

        //Object being interacted
        private InteractBase interacting;
        [SerializeField] private bool interactOnCollision;

        //Attach references
        [SerializeField] private Transform m_attach;
        public Transform attach { get { return m_attach; } }

        public void Activate()
        {
            if (interacting == null) { return; }
            ActivateArg args = new(this, interacting);
            interacting.Activate(args);
            ActivateEnter?.Invoke(args);
        }

        public void CancelActivate()
        {
            if (interacting == null) { return; }
            ActivateArg args = new(this, interacting);
            interacting.Deactivate(args);
            ActivateCancelled?.Invoke(args);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(this.transform.name + "/" + collision.relativeVelocity.magnitude);
            InteractByCollision(collision.collider);
        }

        private void OnCollisionExit(Collision collision)
        {
            CancelInteractByCollision(collision.collider);
        }

        private void OnTriggerEnter(Collider other)
        {
            InteractByCollision(other);
        }

        private void OnTriggerExit(Collider other)
        {
            CancelInteractByCollision(other);
        }

        public void InteractByCollision(Collider collision)
        {
            if (!interactOnCollision) { return; }

            if (collision.attachedRigidbody && collision.attachedRigidbody.TryGetComponent<InteractBase>(out InteractBase interactBase)
                || collision.TryGetComponent<InteractBase>(out interactBase))
            {
                Interact(interactBase);
            }
        }

        public void Interact(InteractBase interact)
        {
            if (InteractionManager.Register(new InteractionArg(this, interact)))
            {
                interacting = interact;
            }

        }

        public void CancelInteraction()
        {
            if (InteractionManager.Remove(new InteractionArg(this, interacting)))
            {
                interacting = null;
            }
        }

        public void CancelInteractByCollision(Collider collision)
        {
            if (!interactOnCollision) { return; }
            if (collision.attachedRigidbody && collision.attachedRigidbody.TryGetComponent<InteractBase>(out InteractBase interactBase)
                || collision.TryGetComponent<InteractBase>(out interactBase))
            {
                CancelInteraction();
            }
        }
    }
}
