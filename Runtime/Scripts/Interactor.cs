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
            ActivateArg args = new(this.transform, interacting);
            interacting.Activate(args);
            ActivateEnter?.Invoke(args);
        }
        public void CancelActivate()
        {
            if (interacting == null) { return; }
            ActivateArg args = new(this.transform, interacting);
            interacting.Deactivate(args);
            ActivateCancelled?.Invoke(args);
        }
    }
}
