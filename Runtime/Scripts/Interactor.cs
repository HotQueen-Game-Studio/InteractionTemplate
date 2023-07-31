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


        public void Interact(InteractBase interact)
        {
            InteractionArg args = new(this, interact);
            interact.Interact(args);
            InteractEnter?.Invoke(args);
            interacting = interact;
        }

        public void CancelInteract()
        {
            if (interacting == null) { return; }
            InteractionArg args = new(this, interacting);
            interacting.StopInteraction(args);
            InteractionCancelled?.Invoke(args);
            interacting = null;
        }

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

            if (collision.attachedRigidbody && collision.attachedRigidbody.TryGetComponent<InteractBase>(out InteractBase interactBase))
            {
                Interact(interactBase);
            }
            else if (collision.TryGetComponent<InteractBase>(out interactBase))
            {
                Interact(interactBase);
            }
        }

        public void CancelInteractByCollision(Collider collision)
        {
            if (!interactOnCollision) { return; }
            if (collision.attachedRigidbody.TryGetComponent<InteractBase>(out InteractBase interactBase))
            {
                CancelInteract();
            }
            else if (collision.TryGetComponent<InteractBase>(out interactBase))
            {
                CancelInteract();
            }
        }
    }
}
