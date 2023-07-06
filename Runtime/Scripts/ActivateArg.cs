using UnityEngine;

namespace HotQueen.Interaction
{
    public struct ActivateArg
    {
        public Interactor interactor;
        public IActivate interacted;

        public ActivateArg(Transform interactor, IActivate interacted)
        {
            this.interactor = interactor;
            this.interacted = interacted;
        }
    }
}
