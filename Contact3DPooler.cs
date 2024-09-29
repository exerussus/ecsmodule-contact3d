using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace ECS.Modules.Exerussus.Contact3D
{
    public class Contact3DPooler : IGroupPooler
    {
        public void Initialize(EcsWorld world)
        {
            ContactDetector = new PoolerModule<Contact3DData.ContactDetector>(world);
            ReadOnlyCollisionHandler = new PoolerModule<ReadOnlyContact3DData.CollisionHandler>(world);
            HandlerFilter = world.Filter<ReadOnlyContact3DData.CollisionHandler>().End();
        }

        public bool IsDebug;
        public List<Collision3DProcess> ProcessesDebug;
        public EcsFilter HandlerFilter;
        public PoolerModule<Contact3DData.ContactDetector> ContactDetector;
        public PoolerModule<ReadOnlyContact3DData.CollisionHandler> ReadOnlyCollisionHandler;
    }
}