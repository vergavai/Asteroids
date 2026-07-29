using UnityEngine;

namespace Project.Code.UI.Visibility
{
    public class InputVisibility : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private ForceMobileCheck _forceMobile;

        private void Awake()
        {
            bool useMobile = _forceMobile.ForceMobileInput || SystemInfo.deviceType == DeviceType.Handheld;
            
            if (!useMobile)
            {
                _canvasGroup.blocksRaycasts = false;
                _canvasGroup.interactable = false;
                _canvasGroup.alpha = 0;
            }
            else
            {
                _canvasGroup.blocksRaycasts = true;
                _canvasGroup.interactable = true;
                _canvasGroup.alpha = 1;
            }
        }
    }
}