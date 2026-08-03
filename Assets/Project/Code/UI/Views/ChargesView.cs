using MVVM;
using TMPro;
using UnityEngine;

namespace Project.Code.UI.Views
{
    public class ChargesView : MonoBehaviour
    {
        [Data("Charges")]
        public TMP_Text _chargesText;
        
        public TMP_Text ChargesText => _chargesText;
    }
}