using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Ayarlar")]
    public Transform player;
    public TextMeshProUGUI scoreText;

    [Header("Düşmanlar (Testere)")]
    public GameObject sawPrefab;
    public float sawInterval = 50f;

    [Header("Bonuslar (Boost)")]
    public GameObject boostPrefab;
    public float boostInterval = 75f;

    private float heightScore = 0f;
    private float bonusScore = 0f;
    
    private float nextSawScore = 50f;
    private float nextBoostScore = 75f;

    void Update()
    {
        // 1. SKOR HESABI
        if (player.position.y > heightScore)
        {
            heightScore = player.position.y;
        }
        float totalScore = heightScore + bonusScore;

        scoreText.text = "Skor: " + Mathf.Round(totalScore).ToString();

        // 2. HIGH SCORE KAYIT
        float kayitliSkor = PlayerPrefs.GetFloat("EnYuksekSkor", 0);
        if (totalScore > kayitliSkor)
        {
            PlayerPrefs.SetFloat("EnYuksekSkor", totalScore);
        }

        // --- DÜZELTİLEN KISIM BURASI ---
        
        // 3. TESTERE OLUŞTURMA
        if (totalScore >= nextSawScore)
        {
            SpawnObject(sawPrefab);
            // ARTIK PANİK YOK: "Şu anki skorun üzerine 50 ekle, bir sonraki o zaman çıksın" diyoruz.
            nextSawScore = totalScore + sawInterval; 
        }

        // 4. BOOST OLUŞTURMA
        if (totalScore >= nextBoostScore)
        {
            SpawnObject(boostPrefab); 
            // Aynı mantığı buraya da uyguladık.
            nextBoostScore = totalScore + boostInterval;
        }
    }

    public void AddScore(float amount)
    {
        bonusScore += amount;
    }

    // Testere ve Boost için ortak oluşturma fonksiyonu (Çarpışma Engellemeli)
    void SpawnObject(GameObject prefabToSpawn)
    {
        Vector3 spawnPos = Vector3.zero;
        bool uygunYerBulundu = false;

        for (int i = 0; i < 10; i++)
        {
            float randomX = Random.Range(-2.2f, 2.2f);
            float randomYOffset = Random.Range(10f, 11.5f); 
            spawnPos = new Vector3(randomX, player.position.y + randomYOffset, 0);

            Collider2D hit = Physics2D.OverlapCircle(spawnPos, 0.5f);

            if (hit == null)
            {
                uygunYerBulundu = true;
                break; 
            }
        }

        if (uygunYerBulundu)
        {
            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.Log("Uygun yer bulunamadı, spawn iptal edildi."); 
        }
    }
}