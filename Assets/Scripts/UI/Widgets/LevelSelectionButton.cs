using Assets.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace Assets.Scripts.UI.Widgets
{
    public class LevelSelectionButton : MonoBehaviour
    {
        [SF] private TMP_Text levelText;
        [SF] private Button button;

        private int levelIndex;
        private IGameplayManager gameplayManager;

        [Inject]
        private void Construct(IGameplayManager gameplayManager)
        {
            this.gameplayManager = gameplayManager;
        }

        public void Setup(int levelIndex)
        {
            this.levelIndex = levelIndex;
            levelText.text = $"LEVEL {levelIndex + 1}";

            transform.localScale = Vector3.one;
        }

        private void ClickAction()
        {
            gameplayManager.StartLevel(levelIndex);
        }

        private void OnEnable()
        {
            button.onClick.AddListener(ClickAction);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(ClickAction);
        }
    }
}
