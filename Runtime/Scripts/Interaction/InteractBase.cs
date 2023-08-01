using System;
using UnityEngine;
using UnityEngine.Events;

namespace HotQueen.Interaction
{
    public class InteractBase : MonoBehaviour, IInteract, IActivate
    {
        //unity event
        [SerializeField] private UnityEvent<InteractionArg> Interacted;
        [SerializeField] private UnityEvent<InteractionArg> StoppedInteraction;
        [SerializeField] private UnityEvent<ActivateArg> Activated;
        [SerializeField] private UnityEvent<ActivateArg> Deactivated;

        public Transform transform { get { return base.transform; } }

        //action
        public event Action<InteractionArg> interacted;
        public event Action<InteractionArg> stoppedInteraction;
        public event Action<ActivateArg> activated;
        public event Action<ActivateArg> deactivated;

        private void Awake()
        {
            interacted += (ctx) => Interacted?.Invoke(ctx);
            stoppedInteraction += (ctx) => StoppedInteraction?.Invoke(ctx);
            activated += (ctx) => Activated?.Invoke(ctx);
            deactivated += (ctx) => Deactivated?.Invoke(ctx);
        }

        public virtual void Activate(ActivateArg args)
        {
            activated?.Invoke(args);
        }

        public virtual void Deactivate(ActivateArg args)
        {
            deactivated?.Invoke(args);
        }

        public virtual void Interact(InteractionArg args)
        {
            interacted?.Invoke(args);
        }

        public virtual void StopInteraction(InteractionArg args)
        {
            stoppedInteraction?.Invoke(args);
        }
    }
}
