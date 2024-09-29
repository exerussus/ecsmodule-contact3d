using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Modules.Exerussus.Contact3D
{
    public static class Contact3DSignals
    {
        /// <summary>
        /// Срабатывает при контакте ContactDetector с другим ContactDetector
        /// </summary>
        public struct OnContactEntity
        {
            /// <summary> Уникальный идентификатор столкновения </summary>
            public int CollisionId;
            /// <summary> Первая запакованная сущность </summary>
            public EcsPackedEntity First;
            public Collider FirstCollider;
            /// <summary> Вторая запакованная сущность </summary>
            public EcsPackedEntity Second;
            public Collider SecondCollider;
        }
        
        /// <summary>
        /// Срабатывает при контакте ContactDetector с объектом на сцене, который не обладает ContactDetector.
        /// </summary>
        public struct OnContactCollider
        {
            /// <summary> Уникальный идентификатор столкновения </summary>
            public int CollisionId;
            /// <summary> Первая запакованная сущность </summary>
            public EcsPackedEntity First;
            public Collider FirstCollider;
            public Collider SecondCollider;
        }
    }
}