using System;
using ECS.Modules.Exerussus.Contact3D.MonoBehaviours;
using Exerussus._1EasyEcs.Scripts.Core;
using UnityEngine;

namespace ECS.Modules.Exerussus.Contact3D
{
    public static class Contact3DExtensions
    {
        public static void AddCollisionProcess(this Contact3DPooler pooler, Contact3DDetector first, Contact3DDetector second)
        {
            if (pooler.HandlerFilter.TryGetFirstEntity(out var handlerEntity))
            {
                ref var handlerData = ref pooler.ReadOnlyCollisionHandler.Get(handlerEntity);

                var hashCode = GenerateHash(first.ID, second.ID);
                    
                if (handlerData.ExistingProcessesHash.Contains(hashCode)) return;

                var process = new Collision3DProcess
                {
                    HashCode = hashCode,
                    First = new EntityInfo { Collider = first.Collider, Entity = first.EcsPackedEntity, HasEntity = true },
                    Second = new EntityInfo { Collider = second.Collider, Entity = second.EcsPackedEntity, HasEntity = true }
                };
                
                handlerData.Processes.Enqueue(process);
                handlerData.ExistingProcessesHash.Add(hashCode);
                if (pooler.IsDebug) pooler.ProcessesDebug.Add(process);
            }
        }
        public static void AddCollisionProcess(this Contact3DPooler pooler, Contact3DDetector first, Collider second)
        {
            if (pooler.HandlerFilter.TryGetFirstEntity(out var handlerEntity))
            {
                ref var handlerData = ref pooler.ReadOnlyCollisionHandler.Get(handlerEntity);

                var hashCode = GenerateHash(first.ID, second.GetHashCode());
                
                if (handlerData.ExistingProcessesHash.Contains(hashCode)) return;

                var process = new Collision3DProcess
                {
                    HashCode = hashCode,
                    First = new EntityInfo { Collider = first.Collider, Entity = first.EcsPackedEntity, HasEntity = true },
                    Second = new EntityInfo { Collider = second, HasEntity = false }
                };
                
                handlerData.Processes.Enqueue(process);
                handlerData.ExistingProcessesHash.Add(hashCode);
                if (pooler.IsDebug) pooler.ProcessesDebug.Add(process);
            }
        }
        
        public static int GenerateHash(int firstId, int secondId)
        {
            var minId = Math.Min(firstId, secondId);
            var maxId = Math.Max(firstId, secondId);

            unchecked
            {
                var hash = 17;
                hash = hash * 31 + minId;
                hash = hash * 31 + maxId;
                return hash;
            }
        }
    }
}