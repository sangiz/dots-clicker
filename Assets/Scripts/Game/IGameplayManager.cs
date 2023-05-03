using Assets.Scripts.Game.Level;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public interface IGameplayManager
    {
        event Action<int> OnPointConnected;
        event Action<int> OnPointClicked;

        List<Vector2> GetLevelPointsCoordinates(int levelIndex);
    }
}
