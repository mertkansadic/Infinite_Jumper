using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Platform Prefablari")]
    public GameObject obstacle;        // Normal platform
    public GameObject BrokenPlatform;  // Kirilan
    public GameObject MovingPlatforms; // Hareketli
    public GameObject SpringPlatform;  // Ziplayan

    [Header("Ayarlar")]
    public Transform playerTransform;
    public float distanceBetween = 2.2f;
    public float widthLimit = 1.8f;

    private float lastSpawnY = -3f;
    private int platformCount = 0; 

    void Update()
    {
        if (playerTransform != null && playerTransform.position.y + 10f > lastSpawnY)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        lastSpawnY += distanceBetween;
        platformCount++; 

        Vector3 spawnPos = new Vector3(Random.Range(-widthLimit, widthLimit), lastSpawnY, 0);
        GameObject selectedPrefab = obstacle; // Varsayilan olarak normal platform

        // --- ZORLUK MANTIGI ---
        if (platformCount < 10) 
        {
            selectedPrefab = obstacle;
        }
        else if (platformCount < 25) 
        {
            selectedPrefab = (Random.value > 0.5f) ? obstacle : BrokenPlatform;
        }
        else if (platformCount < 50) 
        {
            float r = Random.value;
            if (r < 0.4f) selectedPrefab = obstacle;
            else if (r < 0.7f) selectedPrefab = BrokenPlatform;
            else selectedPrefab = MovingPlatforms;
        }
        else 
        {
            float r = Random.value;
            if (r < 0.3f) selectedPrefab = obstacle;
            else if (r < 0.6f) selectedPrefab = BrokenPlatform;
            else if (r < 0.85f) selectedPrefab = MovingPlatforms;
            else selectedPrefab = SpringPlatform;
        }

        // Objeyi sahneye kopyala
        if (selectedPrefab != null)
        {
            Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        }
    }
}