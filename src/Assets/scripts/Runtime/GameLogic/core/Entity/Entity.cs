using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameLogic
{
    public class Entity : MonoBehaviour {
        protected int entityId_;
        protected GameObject gameObject_;
        protected EntityType entityType_;




        protected Vector3 logicPosition_;
        protected Quaternion logicRotation_;


        protected Vector3 birthPosition_;
        protected Quaternion birthRotation_;

        protected Vector3 DestroyPosition_;
        protected Quaternion DestroyRotation_;

        protected int DestroyFrameId_;
        protected int birthFrameId_;


        public int EntityId { get { return entityId_; } }
        public GameObject GameObject { get { return gameObject_; } }
        public EntityType EntityType { get { return entityType_; } }

        public virtual void logicUpdate() {

        }

        public void setLogicPosition(Vector3 vector3) {
            logicPosition_ = vector3;
            gameObject_.transform.position = vector3;
        }

        public void setLogicRotation(Quaternion quaternion) { 
            logicRotation_ = quaternion;
            gameObject_.transform.rotation = quaternion;
        }
    }
}
