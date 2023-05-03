using Assets.Scripts.Game;
using UnityEngine;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace Assets.Scripts.UI.Widgets
{
    public class ConnectablePointWidget : MonoBehaviour
    {
        [SF] private RectTransform rectTransform;

        private int pointIndex;

        [Inject]
        private void Construct(IGameplayManager gameplayManager)
        {
        }

        public void Setup(int pointIndex, Vector3 position)
        {
            this.pointIndex = pointIndex;

            rectTransform.anchoredPosition3D = position;
            transform.localScale = Vector3.one;
        }

        private void OnClicked()
        {
        }
    }
}
