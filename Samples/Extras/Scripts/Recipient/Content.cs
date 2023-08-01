using UnityEngine;

namespace Cittius.Tools
{
    [System.Serializable]
    public struct Content
    {
        public string name;
        [Range(0, 100)] public int amount;

        public Content(string contentName, int amount)
        {
            this.name = contentName;
            this.amount = amount;
        }
    }
}
