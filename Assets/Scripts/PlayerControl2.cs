using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl2 : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float jumpForce = 5.0f; 
    public float Speed = 5.0f;
    public float tiltSensitivity = 2.0f; // Telefonu ne kadar eğince hareket etsin?

    private float moveDirection;
    private bool jumpRequest;
    private bool Grounded = true;

    private Rigidbody2D rb;
    private SpriteRenderer renderer;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // --- 1. HAREKET GİRDİSİ (Hem Klavye Hem Telefon Sensörü) ---
        
        // Bilgisayar için Klavye girdisi (A/D veya Ok Tuşları)
        float keyboardInput = Input.GetAxisRaw("Horizontal");

        // Telefon için İvmeölçer (Tilt/Sallama) girdisi
        // Input.acceleration.x telefonun sağa/sola eğimini verir (-1 ile 1 arası)
        float tiltInput = Input.acceleration.x * tiltSensitivity;

        // İkisini topluyoruz ki hem PC'de hem Tel'de çalışsın
        moveDirection = keyboardInput + tiltInput;

        // Karakterin yönünü çevir (Yüzünü sağa/sola dönmesi)
        if (Mathf.Abs(moveDirection) > 0.1f) // Çok küçük titremelerde dönmesin diye eşik koyduk
        {
            renderer.flipX = moveDirection < 0;
        }

        // Animasyon hızı
        animator.SetFloat("Speed", Mathf.Abs(moveDirection) * Speed);


        // --- 2. ZIPLAMA GİRDİSİ (Hem Klavye Hem Dokunmatik) ---
        
        // Input.GetMouseButtonDown(0) mobilde "Ekrana Dokunma" anlamına gelir
        if (Grounded && (Input.GetKeyDown(KeyCode.W) || Input.GetMouseButtonDown(0)))
        {
            jumpRequest = true;
        }

        // --- 3. EKRAN SINIRLAMA ---
        float solSinir = -2.1f; 
        float sagSinir = 2.1f;
        float sinirliX = Mathf.Clamp(transform.position.x, solSinir, sagSinir);
        transform.position = new Vector3(sinirliX, transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        // Yatay hareket
        // moveDirection'ı Speed ile çarparak hızı uyguluyoruz
        rb.linearVelocity = new Vector2(moveDirection * Speed, rb.linearVelocity.y);

        // Zıplama uygulama
        if (jumpRequest)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequest = false;
            Grounded = false;

            animator.SetTrigger("Jump");
            animator.SetBool("Grounded", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Zemin") || col.gameObject.CompareTag("Platform")) 
        {
            // Çarptığımız yer karakterin altındaysa zemin sayalım
            // (Basit bir kontrol, platformun yanına çarpınca zıplamaması için)
            if (col.relativeVelocity.y > 0) 
            {
                Grounded = true;
                animator.SetBool("Grounded", true);
            }
        }
    }

    private void LateUpdate()
    {
        // Kamera Takibi
        if (Camera.main != null && transform.position.y > Camera.main.transform.position.y)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, -10f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Deadly") || other.name == "DeadZone")
        {
            // İkisinden biri olduysa menüye dön
            SceneManager.LoadScene("Menu");
        }
        if (other.gameObject.CompareTag("Boost"))
        {
            // Sahnedeki ScoreManager'ı bul
            ScoreManager sm = FindFirstObjectByType<ScoreManager>();
            
            // Eğer bulduysan 100 puan ekle
            if (sm != null)
            {
                sm.AddScore(100f);
            }

            // O objeyi yok et (Yedik çünkü)
            Destroy(other.gameObject);
        }
    }
}