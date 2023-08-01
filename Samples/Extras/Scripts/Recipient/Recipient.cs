using HotQueen.Interaction;
using UnityEngine;
using UnityEngine.Events;

namespace Cittius.Tools
{
    public class Recipient : MonoBehaviour
    {
        [Header("Recipient Values")]
        [SerializeField] private Content m_content;
        public Content content { get { return m_content; } }
        public bool emptyAfterUse = true;
        public bool canBeFilled = false;
        public bool canTransfer = false;
        public InteractBase interact;

        [Header("Recipient Events")]
        public UnityEvent onEmptied;
        public UnityEvent<Content> onFilled;
        //public UnityEvent<Recipient> onTranfered;


        private void Start()
        {
            interact.activated += (args) =>
            {
                IInteract[] interactions = InteractionManager.Find(args.interactor);
                foreach (var item in interactions)
                {
                    if (item.transform.TryGetComponent<Recipient>(out Recipient recipient))
                    {
                        Fill(recipient.content);
                    }
                }
            };
        }

        public void Fill(Content content)
        {
            if (canBeFilled && this.content.name == content.name || this.content.name == "")
            {
                Debug.Log(this.transform.name + " Filled with " + content.name);
                m_content.name = content.name;
                m_content.amount += content.amount;
                onFilled?.Invoke(content);
            }

        }

        //public void Transfer(Recipient recipient)
        //{
        //    if (canTransfer && m_content.amount != 0)
        //    {

        //        Debug.Log(this.transform.name + " Tramsfering to " + recipient.transform.name);
        //        recipient.Fill(this.content);
        //        onTranfered?.Invoke(recipient);
        //        if (emptyAfterUse)
        //        {
        //            m_content.amount = 0;
        //            onEmptied?.Invoke();
        //        }
        //    }
        //}

        //public void Transfer(InteractionArg args)
        //{
        //    if (args.interacted.transform.TryGetComponent<Recipient>(out Recipient recipient))
        //    {
        //        Transfer(recipient);
        //    }
        //}
    }
}
