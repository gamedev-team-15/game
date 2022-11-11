using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    [SerializeField]
    public float speed = 3f;
    Rigidbody2D rb;
    Vector2 moveDirection;
    private Transform target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
        }
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle  = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        }
    }
}
