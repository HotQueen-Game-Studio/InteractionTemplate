using UnityEngine;

namespace HotQueen.Interaction
{

    public class Interactor : MonoBehaviour
    {
        //Object being interacted
        private InteractBase interacting;
        [SerializeField] private bool interactOnCollision;

        //Attach references
        [SerializeField] private Transform m_attach;
        public Transform attach { get { return m_attach; } }

        //Activate

        public void Activate()
        {
            InteractionManager.Activate(this);
        }

        public void CancelActivate()
        {
            InteractionManager.Deactivate(this);
        }

        //Interaction
        public void Interact(InteractBase interact)
        {
            InteractionArg arg = new InteractionArg(this, interact);
            if (InteractionManager.Register(arg))
            {
                interacting = interact;
            }

        }

        public void CancelInteraction()
        {
            InteractionArg arg = new InteractionArg(this, interacting);
            if (InteractionManager.Remove(arg))
            {
                interacting = null;
            }
        }

        //Interaction By Collision

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

            if (collision.attachedRigidbody && collision.attachedRigidbody.TryGetComponent<InteractBase>(out InteractBase interactBase)
                || collision.TryGetComponent<InteractBase>(out interactBase))
            {
                Interact(interactBase);
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
