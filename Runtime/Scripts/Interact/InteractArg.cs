using UnityEngine;

namespace HotQueen.Interaction
{
    public struct InteractionArg
    {
        public Transform interactor;
        public IInteract interacted;

        public InteractionArg(Transform interactor, IInteract interacted)
        {
            this.interactor = interactor;
            this.interacted = interacted;
        }
    }
}
