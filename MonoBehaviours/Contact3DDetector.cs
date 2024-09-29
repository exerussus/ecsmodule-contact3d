using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Modules.Exerussus.Contact3D.MonoBehaviours
{
    public class Contact3DDetector : MonoBehaviour
    {
        [SerializeField] private bool canInvokeSignals;
        [SerializeField] private bool canTouchNotEntity;
        [SerializeField] private Collider objectCollider;
        private Signal _signal;
        private Contact3DPooler _pooler;
        
        public EcsPackedEntity EcsPackedEntity { get; private set; }
        public Collider Collider => objectCollider;
        public Dictionary<Collider, Contact3DDetector> Detectors { get; private set; }
        public bool HasOtherDetector { get; private set; }
        public bool IsInitialized { get; private set; }
        public int ID { get; private set; }
        
        public void Initialize(
            EcsPackedEntity ecsPackedEntity, 
            Signal signal, 
            Contact3DPooler contactPooler)
        {
            EcsPackedEntity = ecsPackedEntity;
            _signal = signal;
            _pooler = contactPooler;

            if (_pooler.HandlerFilter.TryGetFirstEntity(out var handlerEntity))
            {
                ref var handlerData = ref _pooler.ReadOnlyCollisionHandler.Get(handlerEntity);
                Detectors = handlerData.Detectors;
                Detectors[objectCollider] = this;
                ID = handlerData.DetectorIdCounter.FreeId;
                IsInitialized = true;
            }
        }

        public void Deinitialize()
        {
            if (IsInitialized && _pooler != null && Detectors.ContainsKey(objectCollider))
            {
                Detectors.Remove(objectCollider);
                IsInitialized = false;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!IsInitialized) return;
            HasOtherDetector = false;
            if (!canInvokeSignals) return;
            
            if (Detectors.TryGetValue(other.collider, out var otherDetector)) _pooler.AddCollisionProcess(this, otherDetector);
            else if (canTouchNotEntity) _pooler.AddCollisionProcess(this, other.collider);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsInitialized) return;
            HasOtherDetector = false;
            if (!canInvokeSignals) return;
            
            if (Detectors.TryGetValue(other, out var otherDetector)) _pooler.AddCollisionProcess(this, otherDetector);
            else if (canTouchNotEntity) _pooler.AddCollisionProcess(this, other);
        }

        private void OnDestroy()
        {
            Deinitialize();
        }
    }
}
