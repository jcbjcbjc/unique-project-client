using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class LiveEntity:Entity
    {
        protected bool IsDeath_;
        protected int CreationFrameId_;
        protected int deathFrameId_;

        protected AnimationController animationController_;
        protected AiManager aiManager_;


    }
}
