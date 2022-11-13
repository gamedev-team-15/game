using UnityEngine;

namespace Core
{
    public class RuntimeManager : MonoBehaviour
    {
        public static RuntimeManager Instance { get; private set; }
        public static Utils.Utils Utils { private set; get; }

        private void Start()
        {
            if (Instance)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            Utils = new Utils.Utils();
        }
    
        
    }
}
