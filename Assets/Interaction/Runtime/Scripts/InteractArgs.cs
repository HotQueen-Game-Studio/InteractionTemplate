using UnityEngine;

namespace HotQueen.Interaction
{
    public struct InteractionArgs
    {
        public Transform interactor;
        public IInteract interacted;

        public InteractionArgs(Transform interactor, IInteract interacted)
        {
            this.interactor = interactor;
            this.interacted = interacted;
        }
    }
}
