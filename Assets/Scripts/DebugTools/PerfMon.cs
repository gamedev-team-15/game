using TMPro;
using UnityEngine;

namespace DebugTools
{
    public class PerfMon : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI field;
    
        void Update()
        {
            field.text = $"T: {Time.deltaTime} ms\nFPS: {1f / Time.deltaTime}";
        }
    }
}
