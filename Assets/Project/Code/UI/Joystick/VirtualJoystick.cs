using Project.Code.Gameplay.Player.InputReading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Project.Code.UI.Joystick
{
    public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _handle;

        [SerializeField] private float _handleRange = 1f;
        [SerializeField] private float _deadZone = 0.1f;

        private MobileInputProvider _inputProvider;
        private Vector2 _inputVector;

        [Inject]
        private void Construct([InjectOptional] MobileInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_inputProvider == null) return;
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_inputProvider == null) return;

            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _background.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out localPoint);

            Vector2 normalized = localPoint / (_background.rectTransform.sizeDelta.x * 0.5f);
            _inputVector = Vector2.ClampMagnitude(normalized, 1f);

            if (_inputVector.sqrMagnitude < _deadZone * _deadZone)
                _inputVector = Vector2.zero;

            Vector2 handlePosition = _inputVector * (_background.rectTransform.sizeDelta.x * 0.5f * _handleRange);
            _handle.rectTransform.anchoredPosition = handlePosition;

            _inputProvider.SetMovementInput(_inputVector);
            _inputProvider.SetAimInput(_inputVector);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_inputProvider == null) return;

            _inputVector = Vector2.zero;
            _handle.rectTransform.anchoredPosition = Vector2.zero;

            _inputProvider.SetMovementInput(Vector2.zero);
            _inputProvider.SetAimInput(Vector2.zero);
        }
    }
}