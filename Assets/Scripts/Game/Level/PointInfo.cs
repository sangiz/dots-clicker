using System;
using UnityEngine;

namespace Assets.Scripts.Game.Level
{
    [Serializable]
    public class PointInfo
    {
        public bool ClickCompleted;
        public bool ConnectionCompleted;
        public Vector2 Position;
    }
}
