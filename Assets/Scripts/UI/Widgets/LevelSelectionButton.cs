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


        public void Setup(int levelIndex)
        {
            this.levelIndex = levelIndex;
            levelText.text = $"LEVEL {levelIndex + 1}";

            transform.localScale = Vector3.one;
        }
    }
}
