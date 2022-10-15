using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace GameLogic
{
    public class EntityManager:Service
    {
        Dictionary<int, Entity> entityMap = new Dictionary<int, Entity>();

        public void Clear() { 
            entityMap.Clear();
        }

        public void AddEntity(Entity entity,int entityId) {
            entityMap[entityId] = entity;
        }
        public Entity GetEntity(int entityId) { 
            return entityMap[entityId];
        }
        public void RemoveEntity(int entityId) { entityMap.Remove(entityId); }

        public void update() {
            foreach (Entity entity in entityMap.Values) {
                if (entity.EntityType != EntityType.Character) {
                    entity.logicUpdate();
                }
            }
        }
    }
}
