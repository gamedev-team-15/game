using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FIcon : MonoBehaviour
    {
        [SerializeField] private Image fillBorder;
        [SerializeField] private Image icon;

        public float FillAmount
        {
            get => fillBorder.fillAmount;
            set => fillBorder.fillAmount = value;
        }

        public Sprite Icon
        {
            get => icon.sprite;
            set => icon.sprite = value;
        }
    }
}