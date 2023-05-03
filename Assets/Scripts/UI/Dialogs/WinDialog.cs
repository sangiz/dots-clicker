using Assets.Scripts.Game;
using Assets.Scripts.UI.Dialogs.Core;
using UnityEngine.UI;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace Assets.Scripts.UI.Dialogs
{
    public class WinDialog : Dialog
    {
        [SF] private Button selectLevelButton;

        private IUiManager uiManager;
        private IGameplayManager gameplayManager;

        [Inject]
        private void Construct(IUiManager uiManager, IGameplayManager gameplayManager)
        {
            this.uiManager = uiManager;
            this.gameplayManager = gameplayManager;

            gameplayManager.OnLevelCompleted += OnLevelCompleted;
            selectLevelButton.onClick.AddListener(OnSelectLevelClicked);
        }

        private void OnDestroy()
        {
            gameplayManager.OnLevelCompleted -= OnLevelCompleted;
            selectLevelButton.onClick.RemoveListener(OnSelectLevelClicked);
        }

        private void OnSelectLevelClicked()
        {
            uiManager.CloseDialog<GameplayDialog>();
            uiManager.OpenDialog<LevelSelectionDialog>();
            Close();
        }

        private void OnLevelCompleted()
        {
            Open();
        }
    }
}
