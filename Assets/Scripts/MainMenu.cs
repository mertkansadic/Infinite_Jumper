using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro için bunu unutma!

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreText; // Menüdeki yazı kutusunu buraya sürükleyeceğiz

    void Start()
    {
        // Oyun açılınca hafızadan skoru çek
        float enYuksek = PlayerPrefs.GetFloat("EnYuksekSkor", 0);
        
        // Yazıya yazdır (Tam sayıya yuvarlayarak)
        highScoreText.text = "En İyi: " + Mathf.Round(enYuksek).ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}