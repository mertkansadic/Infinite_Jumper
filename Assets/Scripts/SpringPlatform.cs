using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    public float springForce = 15.0f; // Normal zıplamanın yaklaşık 2 katı

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Karakterin yukarı doğru hızını kontrol eden Rigidbody'yi bul
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (rb != null && collision.relativeVelocity.y <= 0f)
        {
            // Karakteri yukarı fırlat
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, springForce);
            
            // İstersen burada bir zıplama animasyonu tetikleyebilirsin
            // animator.SetTrigger("Spring");
        }
    }
}