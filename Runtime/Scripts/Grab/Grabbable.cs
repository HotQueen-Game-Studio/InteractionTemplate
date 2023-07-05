using UnityEngine;

namespace HotQueen.Interaction
{
    public class Grabbable : InteractBase
    {
        private Transform attach;

        private void Start()
        {
            interacted += (ctx) =>
            {
                if (ctx.interactor.TryGetComponent<Grabber>(out Grabber grabber))
                {
                    Grab(new GrabArg(grabber, this));
                }
            };

            stoppedInteraction += (ctx) =>
            {
                if (ctx.interactor.TryGetComponent<Grabber>(out Grabber grabber))
                {
                    Drop(new GrabArg(grabber, this));
                }
            };
        }

        private void Grab(GrabArg args)
        {
            attach = args.grabber.attach;
        }
        private void Drop(GrabArg args)
        {
            attach = null;
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
