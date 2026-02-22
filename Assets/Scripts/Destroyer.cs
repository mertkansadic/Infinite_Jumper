using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        // Önce objeyi bulalım
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        
        // Eğer obje bulunduysa transform'u alalım
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Hata: Sahnede 'Player' etiketli bir obje bulunamadı! Lütfen karakterine Player tag'ini ver.");
        }
    }

    void Update()
    {
        // player boş değilse ve mesafe uygunsa yok et
        if (player != null && player.position.y > transform.position.y + 10f)
        {
            Destroy(gameObject);
        }
    }
}