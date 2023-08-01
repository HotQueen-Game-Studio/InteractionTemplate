using System;
using System.Collections.Generic;
using UnityEngine;

namespace HotQueen.Interaction
{
    public static class InteractionManager
    {
        private static List<InteractionArg> m_interactionArgs = new List<InteractionArg>();
        public static event Action<InteractionArg> OnRegister;
        public static event Action<InteractionArg> OnRemoved;
        public static void Register(InteractionArg arg)
        {
            if (
                m_interactionArgs.Contains(arg)
                || arg.interacted.transform.TryGetComponent<Interactor>(out Interactor interactor)
                && arg.interactor.transform.TryGetComponent<InteractBase>(out InteractBase interactBase)
                && m_interactionArgs.Contains(new InteractionArg(interactor, interactBase)
                )) { return; }

            m_interactionArgs.Add(arg);
            OnRegister?.Invoke(arg);
            arg.interacted.Interact(arg);
            Debug.Log("Registered:" + arg.interactor + "/" + arg.interacted);
        }

        public static void Remove(InteractionArg arg)
        {
            if (!m_interactionArgs.Contains(arg)) { return; }


            m_interactionArgs.Remove(arg);
            OnRemoved?.Invoke(arg);
            arg.interacted.StopInteraction(arg);
            Debug.Log("Removed:" + arg.interactor + "/" + arg.interacted);
        }

        public static bool Find(InteractionArg arg, out InteractionArg result)
        {
            result = new InteractionArg();
            foreach (var item in m_interactionArgs)
            {
                if (arg.interacted == item.interacted && arg.interactor == item.interactor)
                {
                    result = item;
                    return true;
                }
            }
            return false;
        }
        public static bool Find(InteractionArg arg)
        {
            return Find(arg, out InteractionArg result);
        }
    }
}
