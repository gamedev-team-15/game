using Player;
using Modifications;
using UnityEngine;

public class TmpModEff : MonoBehaviour
{
    [SerializeField] protected StatModifier tempStatModifier;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out PlayerController p))
        {
            p.ApplyModifier(tempStatModifier);
        }
    }
}
