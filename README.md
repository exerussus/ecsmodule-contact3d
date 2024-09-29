Модуль для 1EasyEcs.   
При наличии на View у сущности класса `Contact3DDetector` - позволяет вызывать сигналы о коллизии с другими сущностями 
или просто коллайдерами (опционально).   

**_Рекомендую подключать группу `Contact3DGroup` перед всеми другими группами, дабы избежать проблем с инициализацией._**

Сигналы на подпись:
````csharp
    public static class Contact3DSignals
    {
        /// <summary>
        /// Срабатывает при контакте ContactDetector с другим ContactDetector
        /// </summary>
        public struct OnContactEntity
        {
            public int CollisionId;
            public EcsPackedEntity First;
            public Collider FirstCollider;
            public EcsPackedEntity Second;
            public Collider SecondCollider;
        }
        
        /// <summary>
        /// Срабатывает при контакте ContactDetector с объектом на сцене, который не обладает ContactDetector.
        /// </summary>
        public struct OnContactCollider
        {
            public int CollisionId;
            public EcsPackedEntity First;
            public Collider FirstCollider;
            public Collider SecondCollider;
        }
    }
````

При касании сущности 1 и сущности 2 генерируется только одно событие о коллизии. 
Это означает, что First не является источником сигнала.
К примеру, может оказаться, что First - это персонаж, а Second - это снаряд. 
Сам снаряд в этом кейсе генерировать сигнал не будет, поэтому стоит проверять, 
что из себя представляет каждая из сущностей в рамках одного сигнала.



Зависимости:  
[Ecs-Lite](https://github.com/Leopotam/ecslite.git)  
[1EasyEcs](https://github.com/exerussus/1EasyEcs.git)   
