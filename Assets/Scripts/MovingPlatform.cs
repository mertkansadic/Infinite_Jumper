using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2.0f;
    public float width = 2.0f; // Ne kadar uzağa gitsin?
    private float startX;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        // Sinüs dalgası kullanarak sağ-sol hareketi yapıyoruz
        float newX = startX + Mathf.Sin(Time.time * speed) * width;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}