using UnityEngine;

namespace HotQueen.Interaction
{
    [RequireComponent(typeof(Interactor))]
    public class Grabber : Interactor
    {
        [SerializeField] private Transform m_attach;
        public Transform attach { get { return m_attach; } }
    }
}
