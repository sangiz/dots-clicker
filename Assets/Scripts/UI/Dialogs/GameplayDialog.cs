using Assets.Scripts.Game;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.UI.Dialogs.Core;
using Assets.Scripts.UI.Widgets;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace Assets.Scripts.UI.Dialogs
{
    public class GameplayDialog : Dialog
    {
        [SF] private Transform pointsHolder;

        private IUiFactory uifactory;
        private IGameplayManager gameplayManager;

        public List<ConnectablePointWidget> Points { get; private set; } = new();

        [Inject]
        private void Construct(IUiManager uiManager, IUiFactory uifactory, IGameplayManager gameplayManager)
        {
            this.uifactory = uifactory;
            this.gameplayManager = gameplayManager;

            uiManager.AddDialog(this);

            gameplayManager.OnLevelStarted += OnLevelStarted;
            gameplayManager.OnPointClicked += OnPointClicked;
        }

        private void OnDestroy()
        {
            gameplayManager.OnPointClicked -= OnPointClicked;
        }

        private void OnLevelStarted()
        {
            Open();
            ClearPoints();
            CreatePoints();
            OrderPoints();
        }

        private void ClearPoints()
        {
            foreach (var p in Points)
            {
                p.gameObject.SetActive(false);
            }

            Points.Clear();
        }

        private void OnPointClicked(int index)
        {
            OrderPoints();
        }

        private void OrderPoints()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                var isClicked = gameplayManager.GetCurrentLevelPoints()[i].ClickCompleted;

                if (!isClicked)
                {
                    Points[i].transform.SetAsLastSibling();
                    return;
                }
            }
        }

        private void CreatePoints()
        {
            var pointsData = gameplayManager.GetCurrentLevelPoints();

            for (var i = 0; i < pointsData.Count; i++)
            {
                var pointWidget = uifactory.CreateConnectablePointWidget();
                Points.Add(pointWidget);

                pointWidget.transform.SetParent(pointsHolder);

                pointWidget.Setup(i, pointsData[i].Position);
            }
        }
    }
}
