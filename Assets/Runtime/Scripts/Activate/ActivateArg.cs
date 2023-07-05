using UnityEngine;

namespace HotQueen.Interaction
{
    public struct ActivateArg
    {
        public Transform interactor;
        public IActivate interacted;

        public ActivateArg(Transform interactor, IActivate interacted)
        {
            this.interactor = interactor;
            this.interacted = interacted;
        }
    }
}
