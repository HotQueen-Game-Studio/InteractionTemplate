namespace Cittius.Tools
{
    [System.Serializable]
    public struct Content
    {
        public string name;
        //public int amount;

        public Content(string contentName)
        {
            this.name = contentName;
        }
    }
}
