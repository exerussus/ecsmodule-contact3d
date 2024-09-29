using System;
using System.Collections.Generic;
using ECS.Modules.Exerussus.Contact3D.MonoBehaviours;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Modules.Exerussus.Contact3D
{
    public static class Contact3DData 
    {
        public struct ContactDetector : IEcsComponent
        {
            public Contact3DDetector Value;
        }
    }
    
    public static class ReadOnlyContact3DData 
    {
        public struct CollisionHandler : IEcsComponent
        {
            public Dictionary<Collider, MonoBehaviours.Contact3DDetector> Detectors;
            public IndexCounter CollisionIdCounter;
            public IndexCounter DetectorIdCounter;
            public Queue<Collision3DProcess> Processes;
            public HashSet<int> ExistingProcessesHash;
        }
    }
    
    [Serializable]
    public struct Collision3DProcess
    {
        public int HashCode;
        
        public EntityInfo First;
        public EntityInfo Second;
    }

    [Serializable]
    public struct EntityInfo
    {
        public bool HasEntity;
        public EcsPackedEntity Entity;
        public Collider Collider;
    }
    
    [Serializable]
    public class IndexCounter
    {
        private int _index;
        
        public int FreeId => _index++;
    }
}
