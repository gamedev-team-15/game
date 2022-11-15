using System;
using Core;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public class InteractableObject : MonoBehaviour
    {
        [SerializeField] private UnityEvent onPlayerInteract = new();
        [SerializeField] private string interactTooltip = "";
        [SerializeField] private Color tooltipColor = Color.yellow;
        public UnityEvent OnPlayerInteract => onPlayerInteract;
        private TextMeshPro _textPopup;

        private void Start()
        {
            _textPopup = RuntimeManager.Utils.CreateTextPopup(transform.position, interactTooltip, Vector3.zero, tooltipColor, 12, -1);
            _textPopup.transform.SetParent(transform);
            _textPopup.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out PlayerController p))
            {
                p.Events.OnPlayerInteract.AddListener(Activate);
                _textPopup.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerController p))
            {
                p.Events.OnPlayerInteract.RemoveListener(Activate);
                _textPopup.gameObject.SetActive(false);
            }
        }

        private void Activate()
        {
            OnPlayerInteract.Invoke();
        }

        public void SetTooltip(bool active)
        {
            _textPopup.gameObject.SetActive(active);
        }

        public void SetTooltipText(string text)
        {
            _textPopup.text = text;
        }

        public void SetTooltipColor(Color color)
        {
            _textPopup.color = color;
        }
    }
}