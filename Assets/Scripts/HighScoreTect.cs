using UnityEngine;
using TMPro;

public class HighScoreTect : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = "HIGHSCORE: " + PlayerPrefs.GetFloat("hs").ToString();
    }
}
