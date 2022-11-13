using UnityEngine;

public class TestShooter : MonoBehaviour
{
    [SerializeField] private Projectile.Projectile2D p;
    [SerializeField] private float interval = 0.5f;
    private float _timer = 0;
    
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (!(_timer < 0)) return;
        var t = Instantiate(p, transform.position, transform.rotation);
        t.Initialize();
        t.Launch(transform.up);
        _timer = interval;
    }
}
