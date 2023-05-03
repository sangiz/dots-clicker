using Assets.Scripts.Game.Level;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public interface IGameplayManager
    {
        event Action OnLevelCompleted;
        event Action OnLevelStarted;
        event Action<int> OnPointConnected;
        event Action<int> OnPointClicked;

        List<Vector2> GetLevelPointsCoordinates(int levelIndex);
        int GetLevelsAmount();
        List<PointInfo> GetCurrentLevelPoints();
        void StartLevel(int level);
        void ConnectPoint(int index);
        bool CanClickPoint(int index);
        bool MarkPointClicked(int index);
        PointInfo GetPointByIndex(int index);
    }
}
