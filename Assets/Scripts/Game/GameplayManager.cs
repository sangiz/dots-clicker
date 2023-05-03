using Assets.Scripts.Game.Level;
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

        [Inject]
        private void Construct()
        {
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
    }
}
