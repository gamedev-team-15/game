using Interfaces;
using UnityEngine;

namespace Editor
{
    public class THealer : MonoBehaviour
    {
        [SerializeField] private int healAmount = 10;

        private void OnTriggerEnter2D(Collider2D col)
        {
            foreach (var behaviour in col.gameObject.GetComponents<MonoBehaviour>())
            {
                if(behaviour is IHeal h)
                    h.ApplyHeal(healAmount);
            }
        }
    }
}
