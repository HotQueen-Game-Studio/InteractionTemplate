namespace HotQueen.Interaction
{
    public struct GrabArg
    {
        public Grabber grabber;
        public Grabbable grabbable;

        public GrabArg(Grabber grabber, Grabbable grabbable)
        {
            this.grabber = grabber;
            this.grabbable = grabbable;
        }
    }
}
