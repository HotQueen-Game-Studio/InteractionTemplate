using System.Collections.Generic;
using UnityEngine;

namespace HotQueen.Interaction
{
    public static class InteractionManager
    {

        public delegate void RegistryEvent<T>(T value) where T : struct;
        //Interaction
        private static List<InteractionArg> m_interactionArgs = new List<InteractionArg>();
        public static event RegistryEvent<InteractionArg> OnRegistered; // Create delegate to registry
        public static event RegistryEvent<InteractionArg> OnRemoved;

        //Activate
        private static List<ActivateArg> m_activateArgs = new List<ActivateArg>();
        private static event RegistryEvent<ActivateArg> OnActivate;
        private static event RegistryEvent<ActivateArg> OnDeactivate;

        public static bool Register(InteractionArg arg)
        {
            if (
                m_interactionArgs.Contains(arg)
                || arg.interacted.transform.TryGetComponent<Interactor>(out Interactor interactor)
                && arg.interactor.transform.TryGetComponent<InteractBase>(out InteractBase interactBase)
                && m_interactionArgs.Contains(new InteractionArg(interactor, interactBase)
                )) { return false; }

            m_interactionArgs.Add(arg);
            OnRegistered?.Invoke(arg);
            arg.interacted.Interact(arg);
            Debug.Log("Registered:" + arg.interactor + "/" + arg.interacted);
            return true;
        }

        public static bool Remove(InteractionArg arg)
        {
            if (!m_interactionArgs.Contains(arg)) { return false; }

            m_interactionArgs.Remove(arg);
            arg.interacted.StopInteraction(arg);
            OnRemoved?.Invoke(arg);
            Debug.Log("Removed:" + arg.interactor + "/" + arg.interacted);
            return true;
        }

        public static bool Activate(Interactor interactor)
        {
            foreach (var item in m_interactionArgs)
            {
                if (item.interactor == interactor && item.interacted.transform.TryGetComponent<IActivate>(out IActivate activate))
                {
                    ActivateArg arg = new ActivateArg(interactor, activate);
                    activate.Activate(arg);
                    m_activateArgs.Add(arg);
                    OnActivate?.Invoke(arg);
                    return true;
                }
            }
            return false;
        }

        public static bool Deactivate(Interactor interactor)
        {
            foreach (var item in m_interactionArgs)
            {
                if (item.interactor == interactor && item.interacted.transform.TryGetComponent<IActivate>(out IActivate activate))
                {
                    ActivateArg arg = new ActivateArg(interactor, activate);
                    activate.Deactivate(arg);
                    m_activateArgs.Remove(arg);
                    OnDeactivate?.Invoke(arg);
                    return true;
                }
            }
            return false;
        }
    }
}
