using MVVM;
using TMPro;
using UnityEngine;

namespace Project.Code.UI.Views
{
    public class RotationAngleView : MonoBehaviour
    {
        [Data("RotationAngle")] 
        public TMP_Text _parameterText;
    }
}