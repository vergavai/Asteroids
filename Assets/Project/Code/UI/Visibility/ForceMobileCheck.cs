using UnityEngine;

namespace Project.Code.UI.Visibility
{
    public class ForceMobileCheck : MonoBehaviour
    {
        [SerializeField] private bool _forceMobileInput;
        
        public bool ForceMobileInput => _forceMobileInput;
    }
}