using System.Collections.Generic;
using UnityEngine;

namespace HotQueen.Interaction
{
    [System.Serializable]
    public struct ReferenceData
    {
        public string Name;
        public string Id;
        public GameObject Prefab;
        public List<GameObject> Instances { get; set; }

        public ReferenceData(string name, string id, GameObject prefab)
        {
            Name = name;
            Id = id;
            Prefab = prefab;
            Instances = new List<GameObject>();
        }
    }
}
