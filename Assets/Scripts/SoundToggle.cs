using UnityEngine;
using TMPro; // Yazıları değiştirmek için

public class SoundToggle : MonoBehaviour
{
    public TextMeshProUGUI butonYazisi; // Butonun içindeki yazı
    private bool sesKapaliMi = false;

    void Start()
    {
        // Oyun açıldığında eski ayarı hatırla (Yoksa 0, yani ses açık kabul et)
        // 1 = Kapalı, 0 = Açık
        sesKapaliMi = PlayerPrefs.GetInt("SesKapali", 0) == 1;
        SesAyariniUygula();
    }

    // Bu fonksiyonu butona basıldığında çalıştıracağız
    public void SesiAcKapat()
    {
        // Durumu tersine çevir (Açıksa kapat, kapalıysa aç)
        sesKapaliMi = !sesKapaliMi;
        
        // Yeni durumu hafızaya kaydet
        PlayerPrefs.SetInt("SesKapali", sesKapaliMi ? 1 : 0);
        
        SesAyariniUygula();
    }

    void SesAyariniUygula()
    {
        if (sesKapaliMi)
        {
            AudioListener.volume = 0f; // Unity'deki tüm sesleri (Müzik+Efekt) sustur
            if(butonYazisi != null) butonYazisi.text = "Ses: Kapalı";
        }
        else
        {
            AudioListener.volume = 1f; // Unity'deki tüm sesleri aç
            if(butonYazisi != null) butonYazisi.text = "Ses: Açık";
        }
    }
}