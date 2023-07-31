using System.Collections.Generic;
using UnityEngine;

namespace HotQueen.Interaction.Extra
{

    [CreateAssetMenu(fileName = "referenceRegistry", menuName = "HotQueen/Registry")]
    public class ReferenceRegistry : ScriptableObject
    {
        [SerializeField] private List<ReferenceData> referenceRegistry = new List<ReferenceData>();
        public void Add(ReferenceData value)
        {
            referenceRegistry.Add(value);
        }

        public ReferenceData Get(string id)
        {
            Get(id, out ReferenceData reference);
            return reference;
        }

        public bool Get(string id, out ReferenceData interactionReference)
        {
            interactionReference = referenceRegistry.Find(r => r.Id == id);
            return interactionReference.Id == id;
        }

        public bool Instantiate<T>(string id = "", string name = "") where T : MonoBehaviour
        {
            if (Get(id, out ReferenceData registry))
            {
                registry.Instances.Add(GameObject.Instantiate(registry.Prefab));
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnEnable()
        {
            foreach (var item in referenceRegistry)
            {
                Debug.Log(item.Prefab.GetInstanceID());
            }
        }
    }

}