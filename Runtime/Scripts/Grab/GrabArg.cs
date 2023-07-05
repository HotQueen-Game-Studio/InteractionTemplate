using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
