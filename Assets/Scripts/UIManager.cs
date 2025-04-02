using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = "SCORE: " + GameManager.instance.totalScore.ToString();
    }
}
