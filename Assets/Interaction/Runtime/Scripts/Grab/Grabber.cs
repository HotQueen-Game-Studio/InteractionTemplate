using UnityEngine;

namespace HotQueen.Interaction
{
    [RequireComponent(typeof(Interactor))]
    public class Grabber : MonoBehaviour
    {
        [SerializeField] private Transform m_attach;
        public Transform attach { get { return m_attach; } }
        public Grabbable grabbed { get; private set; }

        public void Grab(Grabbable grabbable)
        {
            grabbable.Grab(new GrabArg(this, grabbable));
            grabbed = grabbable;
        }

        public void Drop()
        {
            if (grabbed)
            {
                grabbed.Drop(new GrabArg(this, grabbed));
                grabbed = null;
            }
        }
    }
}
