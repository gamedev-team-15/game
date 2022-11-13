using UnityEngine;

namespace Core
{
    public class GameEntity : MonoBehaviour
    {
        public void PlaySoundClip(AudioClip clip)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
        
        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}