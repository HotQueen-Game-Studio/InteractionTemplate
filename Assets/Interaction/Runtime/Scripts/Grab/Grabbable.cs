using UnityEngine;

namespace HotQueen.Interaction
{
    public class Grabbable : MonoBehaviour
    {
        private Transform attach;
        public void Grab(GrabArg args)
        {
            attach = args.grabber.attach;
        }
        public void Drop(GrabArg args)
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
