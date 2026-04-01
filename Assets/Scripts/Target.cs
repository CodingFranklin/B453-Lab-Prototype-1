using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 targetPoint = Vector2.zero;
    private float speed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = GameManager.instance.targetMoveSpeed;
        Vector2 direction = (targetPoint - (Vector2)transform.position).normalized;
        
        float angleOffset = UnityEngine.Random.Range(-30f, 30f);
        direction = Quaternion.Euler(0, 0, angleOffset) * direction;
        
        rb.linearVelocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }
}
