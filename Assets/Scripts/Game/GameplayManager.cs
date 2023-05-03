using Assets.Scripts.Game.Level;
using Assets.Scripts.Infrastructure.Services.JSonReader;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Game
{
    public class GameplayManager : IGameplayManager
    {
        public List<PointInfo> CurrentLevelPoints { get; private set; } = new();

        private Levels levelsData;

        public event Action<int> OnPointClicked;
        public event Action<int> OnPointConnected;
        public event Action OnLevelStarted;
        public event Action OnLevelCompleted;

        [Inject]
        private void Construct(IJsonReaderService jsonReaderservice)
        {
            levelsData = jsonReaderservice.ReadData<Levels>(Constants.Data.LevelsData);
        }

        private bool IsLevelCompleted()
        {
            foreach (var p in CurrentLevelPoints)
            {
                if (!p.ClickCompleted || !p.ConnectionCompleted)
                    return false;
            }

            return true;
        }

        public List<Vector2> GetLevelPointsCoordinates(int levelIndex)
        {
            if (levelIndex > levelsData.levels.Count)
            {
                Debug.LogError($"Level with index {levelIndex} doeas not exist");
                return default;
            }

            var result = new List<Vector2>();
            var level = levelsData.levels[levelIndex];

            for (var i = 0; i < level.level_data.Count; i += 2)
            {
                if (i + 1 >= level.level_data.Count)
                {
                    Debug.LogError($"Coordinates amount have to be even number! Adjust levels data.");
                    return default;
                }

                if (!int.TryParse(level.level_data[i], out int xPos))
                {
                    Debug.LogError("Level data format is wrong!");
                    return default;
                }

                if (!int.TryParse(level.level_data[i + 1], out int yPos))
                {
                    Debug.LogError("Level data format is wrong!");
                    return default;
                }

                var coordinate = new Vector2(xPos, -yPos);
                result.Add(coordinate);
            }

            return result;
        }

        public void StartLevel(int level)
        {
            CurrentLevelPoints.Clear();

            var positions = GetLevelPointsCoordinates(level);

            foreach (var position in positions)
            {
                CurrentLevelPoints.Add(new PointInfo()
                {
                    ClickCompleted = false,
                    Position = position
                });
            }

            OnLevelStarted?.Invoke();
        }

        public void ConnectPoint(int index)
        {
            CurrentLevelPoints[index].ConnectionCompleted = true;
            OnPointConnected?.Invoke(index);

            if (IsLevelCompleted())
            {
                OnLevelCompleted?.Invoke();
            }
        }

        public bool CanClickPoint(int index)
        {
            if (index > CurrentLevelPoints.Count)
                return false;

            if (CurrentLevelPoints[index].ClickCompleted)
                return false;

            for (var i = 0; i < index; i++)
            {
                if (!CurrentLevelPoints[i].ClickCompleted)
                    return false;
            }

            return true;
        }

        public bool MarkPointClicked(int index)
        {
            if (!CanClickPoint(index))
                return false;

            CurrentLevelPoints[index].ClickCompleted = true;
            OnPointClicked?.Invoke(index);

            return true;
        }

        public PointInfo GetPointByIndex(int index)
        {
            if (index >= CurrentLevelPoints.Count)
            {
                Debug.LogError($"Index {index} out of range");
                return CurrentLevelPoints[0];
            }

            if (index < 0)
            {
                Debug.LogError($"Index {index} out of range");
                return CurrentLevelPoints[0];
            }

            return CurrentLevelPoints[index];
        }

        public List<PointInfo> GetCurrentLevelPoints() => CurrentLevelPoints;

        public int GetLevelsAmount()
        {
            return levelsData.levels.Count;
        }
    }
}
