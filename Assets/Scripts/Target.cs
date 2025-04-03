using UnityEngine;

public class Target : MonoBehaviour
{
    public float timeTaken = 0;

    private void Update()
    {
        timeTaken += Time.deltaTime;
        if (timeTaken >= 5)
        {
            Minigame.instance.score -= 20;
            Destroy(gameObject);
        }
        if (GameManager.instance.currentState != GameManager.State.MiniGame)
        {
            Destroy(gameObject);
        }
    }

    public void clicked()
    {
        Minigame.instance.destroyedTargetsCount++;
        Minigame.instance.score += 50 / timeTaken;
        Destroy(gameObject);
    }
}
