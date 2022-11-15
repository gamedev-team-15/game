using System.Collections;
using TMPro;
using UnityEngine;

namespace Core.Utils
{
    public class Utils
    {
        private readonly TextMeshPro _textPopupBase;

        public Utils()
        {
            _textPopupBase = new GameObject("TextPopup").AddComponent<TextMeshPro>();
            _textPopupBase.transform.position = new Vector3(0, 0, 0);
            _textPopupBase.fontSize = 10;
            _textPopupBase.sortingOrder = 100;
            _textPopupBase.alignment = TextAlignmentOptions.Center;
        }

        public TextMeshPro CreateTextPopup(Vector3 position, string text, float fontSize = 12, float fadeTime = 1)
        {
            return CreateTextPopup(position, text, Vector3.up, Color.white, fontSize, fadeTime);
        }

        public TextMeshPro CreateTextPopup(Vector3 position, string text, Vector3 velocity, Color color, float fontSize = 12, float fadeTime = 1)
        {
            var popup = Object.Instantiate(_textPopupBase);
            popup.transform.position = position;
            popup.fontSize = fontSize;
            popup.color = color;
            popup.text = text;
            if (fadeTime > 0)
                popup.StartCoroutine(FadeOut(popup, fadeTime, velocity));
            return popup;
        }

        private static IEnumerator FadeOut(TMP_Text popup, float timeMS, Vector3 velocity)
        {
            var delta = 1f / timeMS;
            var endOfFrame = new WaitForEndOfFrame();
            while (popup.alpha > 0)
            {
                popup.alpha -= Time.deltaTime * delta;
                popup.transform.Translate(velocity * Time.deltaTime);
                yield return endOfFrame;
            }
            yield return null;
            Object.Destroy(popup.gameObject);
        }
    }
}