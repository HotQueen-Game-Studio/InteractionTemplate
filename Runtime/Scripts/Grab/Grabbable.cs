using UnityEngine;
using UnityEngine.Events;

namespace HotQueen.Interaction
{
    public class Grabbable : InteractBase
    {
        public UnityEvent<GrabArg> OnGripped;
        public UnityEvent<GrabArg> OnDropped;
        private Transform attach;

        private void Start()
        {
            interacted += (ctx) =>
            {
                if (ctx.interactor.TryGetComponent<Interactor>(out Interactor grabber))
                {
                    Grab(new GrabArg(grabber, this));
                }
            };

            stoppedInteraction += (ctx) =>
            {
                if (ctx.interactor.TryGetComponent<Interactor>(out Interactor grabber))
                {
                    Drop(new GrabArg(grabber, this));
                }
            };
        }

        private void Grab(GrabArg args)
        {
            attach = args.grabber.attach;
            OnGripped?.Invoke(args);
        }
        private void Drop(GrabArg args)
        {
            attach = null;
            OnDropped?.Invoke(args);
        }
        public void Update()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            if (attach)
            {
                this.transform.position = attach.position;
                this.transform.rotation = attach.rotation;
            }
        }
    }
}
