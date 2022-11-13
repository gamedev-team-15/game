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
            _textPopupBase = new GameObject("tPB").AddComponent<TextMeshPro>();
            _textPopupBase.transform.position = new Vector3(0, 0, 0);
            _textPopupBase.fontSize = 10;
            _textPopupBase.sortingOrder++;
            _textPopupBase.alignment = TextAlignmentOptions.Center;
        }
        
        
        public void CreateTextPopup(Vector3 position, string text, float fadeTime = -1)
        {
            CreateTextPopup(position, text, fadeTime, Vector3.zero);
        }

        public void CreateTextPopup(Vector3 position, string text, float fadeTime, Vector3 velocity)
        {
            var popup = Object.Instantiate(_textPopupBase);
            popup.transform.position = position;
            popup.text = text;
            if (fadeTime > 0)
                popup.StartCoroutine(FadeOut(popup, fadeTime, velocity));
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