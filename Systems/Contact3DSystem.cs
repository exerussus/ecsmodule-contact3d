using System.Collections.Generic;
using ECS.Modules.Exerussus.Contact3D.MonoBehaviours;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Modules.Exerussus.Contact3D.Systems
{
    public class Contact3DSystem : EasySystem<Contact3DPooler>
    {
        private EcsFilter _handlerFilter;
        private List<Collision3DProcess> _processes;
        
        protected override void Initialize()
        {
            _handlerFilter = World.Filter<ReadOnlyContact3DData.CollisionHandler>().End();
            
            var entity = World.NewEntity();
            ref var handlerData = ref Pooler.ReadOnlyCollisionHandler.Add(entity);
            handlerData.Detectors = new Dictionary<Collider, Contact3DDetector>(64);
            handlerData.DetectorIdCounter = new IndexCounter();
            handlerData.CollisionIdCounter = new IndexCounter();
            handlerData.Processes = new Queue<Collision3DProcess>(64);
            handlerData.ExistingProcessesHash = new HashSet<int>(64);
        }

        protected override void Update()
        {
            foreach (var handlerEntity in _handlerFilter)
            {
                ref var handlerData = ref Pooler.ReadOnlyCollisionHandler.Get(handlerEntity);

                if (Pooler.IsDebug) Pooler.ProcessesDebug.Clear();
                handlerData.ExistingProcessesHash.Clear();
                
                for (int i = handlerData.Processes.Count - 1; i >= 0; i--)
                {
                    var collisionProcess = handlerData.Processes.Dequeue();

                    if (collisionProcess.Second.HasEntity)
                    {
                        Signal.RegistryRaise(new Contact3DSignals.OnContactEntity
                        {
                            CollisionId = handlerData.CollisionIdCounter.FreeId,
                            First = collisionProcess.First.Entity,
                            FirstCollider = collisionProcess.First.Collider,
                            Second = collisionProcess.Second.Entity,
                            SecondCollider = collisionProcess.Second.Collider
                        });
                    }
                    else
                    {
                        Signal.RegistryRaise(new Contact3DSignals.OnContactCollider
                        {
                            CollisionId = handlerData.CollisionIdCounter.FreeId,
                            First = collisionProcess.First.Entity,
                            FirstCollider = collisionProcess.First.Collider,
                            SecondCollider = collisionProcess.Second.Collider
                        });
                    }
                }
            }
        }
    }
}