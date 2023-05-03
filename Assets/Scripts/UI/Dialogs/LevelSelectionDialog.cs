using Assets.Scripts.Game;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.UI.Dialogs.Core;
using UnityEngine;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace Assets.Scripts.UI.Dialogs
{
    public class LevelSelectionDialog : Dialog
    {
        [SF] private Transform buttonsHolder;

        private IGameplayManager gameplayManager;

        [Inject]
        private void Construct(IUiManager uiManager, IUiFactory uifactory, IGameplayManager gameplayManager)
        {
            this.gameplayManager = gameplayManager;

            uiManager.AddDialog(this);

            for (var i = 0; i < gameplayManager.GetLevelsAmount(); i++)
            {
                var button = uifactory.CreateLevelSelectionButton();
                button.transform.SetParent(buttonsHolder);
                button.Setup(i);
            }

            gameplayManager.OnLevelStarted += OnlevelStarted;
        }

        private void OnDestroy()
        {
            gameplayManager.OnLevelStarted -= OnlevelStarted;
        }

        private void OnlevelStarted()
        {
            Close();
        }
    }
}
