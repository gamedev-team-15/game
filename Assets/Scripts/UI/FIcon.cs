using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FIcon : MonoBehaviour
    {
        public readonly struct FIconData
        {
            public Sprite Sprite { get; }
            private readonly IValueProvider<float> _fillAmount;

            public FIconData(Sprite sprite, IValueProvider<float> fillInfoProvider)
            {
                Sprite = sprite;
                _fillAmount = fillInfoProvider;
            }

            public float FillAmount => _fillAmount.GetValue();
        }
        
        [SerializeField] private Image fillBorder;
        [SerializeField] private Image icon;
        private FIconData _data;

        public void AttachData(FIconData data)
        {
            _data = data;
            icon.sprite = _data.Sprite;
        }
        
        private void Update()
        {
            SetFill(_data.FillAmount);
        }

        public void SetFill(float amount)
        {
            fillBorder.fillAmount = amount;
        }

        public void SetIcon(Sprite sprite)
        {
            icon.sprite = sprite;
        }
    }
}