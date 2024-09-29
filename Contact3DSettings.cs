using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;

namespace ECS.Modules.Exerussus.Contact3D
{
    public class Contact3DSettings
    {
        public UpdateType Update = UpdateType.FixedUpdate;
        public bool IsDebug = false;
        public List<Collision3DProcess> ProcessesDebug;
    }
}