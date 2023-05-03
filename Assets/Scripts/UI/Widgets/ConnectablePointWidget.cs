using Assets.Scripts.Game;
using Assets.Scripts.UI.Dialogs;
using Assets.Scripts.UI.Dialogs.Core;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace Assets.Scripts.UI.Widgets
{
    public class ConnectablePointWidget : MonoBehaviour
    {
        [SF] private RectTransform rectTransform;
        [SF] private Image image;
        [SF] private Button button;
        [SF] private TMP_Text orderNumberText;

        [SF] private Sprite defaultSprite;
        [SF] private Sprite completedSprite;
        [SF] private LineRenderer lineRenderer;

        [Header("Animation")]
        [SF] private float ropeGrappleAnimationDuration;
        [SF] private float buttonClickScalingDuration;

        private IGameplayManager gameplayManager;
        private IUiManager uiManager;
        private int pointIndex;

        public event Action<ConnectablePointWidget> OnDisabled;

        [Inject]
        private void Construct(IUiManager uiManager, IGameplayManager gameplayManager)
        {
            this.uiManager = uiManager;
            this.gameplayManager = gameplayManager;
        }

        private void OnEnable()
        {
            gameplayManager.OnPointConnected += OnPointStateUpdated;
            gameplayManager.OnPointClicked += OnPointStateUpdated;
            button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            gameplayManager.OnPointConnected -= OnPointStateUpdated;
            gameplayManager.OnPointClicked -= OnPointStateUpdated;
            button.onClick.RemoveListener(OnClicked);

            OnDisabled?.Invoke(this);
        }

        public void Setup(int pointIndex, Vector3 position)
        {
            this.pointIndex = pointIndex;
            var displayLevel = pointIndex + 1;
            image.sprite = defaultSprite;
            orderNumberText.text = $"{displayLevel}";
            orderNumberText.alpha = 1;

            rectTransform.anchoredPosition3D = position;
            transform.localScale = Vector3.one;
        }

        private void OnPointStateUpdated(int index)
        {
            var previousPointIndex = GetPreviousPointIndex();
            var previousPoint = gameplayManager.GetCurrentLevelPoints()[previousPointIndex];
            var isPreviousPointClicked = previousPoint.ClickCompleted;
            var isPreviousPointConnected = index == 1 ? true : previousPoint.ConnectionCompleted;

            if (!isPreviousPointClicked || !isPreviousPointConnected)
                return;

            var previousPointUpdated = previousPointIndex == index;
            var thisPointUpdated = index == pointIndex;
            var canUpdateState = previousPointUpdated || thisPointUpdated;

            if (!canUpdateState)
                return;

            var thisPointConnected = gameplayManager.GetPointByIndex(pointIndex).ConnectionCompleted;
            var thisPointClicked = gameplayManager.GetPointByIndex(pointIndex).ClickCompleted;

            if (thisPointClicked && !thisPointConnected)
            {
                ConnectWithTargetPoint();
            }
        }

        private int GetPreviousPointIndex()
        {
            if (pointIndex == 0)
            {
                return gameplayManager.GetCurrentLevelPoints().Count - 1;
            }

            return pointIndex - 1;
        }

        private void OnClicked()
        {
            if (gameplayManager.CanClickPoint(pointIndex))
            {
                gameplayManager.MarkPointClicked(pointIndex);
                image.sprite = completedSprite;
                PlayClickAnimation();
            }
        }

        private void ConnectWithTargetPoint()
        {
            var isFirstPoint = pointIndex == 0;

            if (isFirstPoint)
            {
                var lastPointIndex = gameplayManager.GetCurrentLevelPoints().Count - 1;
                var previousPoint = uiManager.GetDialog<GameplayDialog>().Points[lastPointIndex];
                PlayRopeGrappleAnimation(previousPoint.transform.position, transform.position);
            }
            else
            {
                var previousPoint = uiManager.GetDialog<GameplayDialog>().Points[pointIndex - 1];
                PlayRopeGrappleAnimation(previousPoint.transform.position, transform.position);
            }
        }

        private void PlayRopeGrappleAnimation(Vector3 fromPosition, Vector3 toPosition)
        {
            var currentToPosition = fromPosition;

            lineRenderer.SetPosition(0, fromPosition);
            lineRenderer.SetPosition(1, fromPosition);

            DOTween.To(() => currentToPosition, x => currentToPosition = x, toPosition, ropeGrappleAnimationDuration).OnUpdate(() =>
            {
                lineRenderer.SetPosition(0, fromPosition);
                lineRenderer.SetPosition(1, currentToPosition);

            }).OnComplete(() =>
            {
                gameplayManager.ConnectPoint(pointIndex);
            }).SetEase(Ease.OutSine);
        }

        private void PlayClickAnimation()
        {
            var clickedScale = Vector3.one * -0.15f;

            rectTransform.DOPunchScale(clickedScale, buttonClickScalingDuration, 0, 0).SetEase(Ease.OutSine);
            orderNumberText.DOFade(0, buttonClickScalingDuration).SetEase(Ease.InSine);
        }
    }
}
