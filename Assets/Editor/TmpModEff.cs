using GameAssets;
using Player;
using Modifications;
using UnityEngine;

public class TmpModEff : MonoBehaviour
{
    [SerializeField] private StatusEffect effect;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out PlayerController p))
        {
            p.ApplyEffect(effect);
        }
    }
}
