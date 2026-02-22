using UnityEngine;

public class BrokenPlatform : MonoBehaviour
{
    public float breakDelay = 1.0f; // Üstüne basınca ne kadar sonra yok olsun?

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Sadece karakterimiz üstüne basarsa kırılsın
        if (collision.gameObject.CompareTag("Player"))
        {
            // Belirlenen süre sonra yok etme fonksiyonunu çağır
            Invoke("Break", breakDelay);
        }
    }

    void Break()
    {
        // Platformu yok et
        Destroy(gameObject);
    }
}