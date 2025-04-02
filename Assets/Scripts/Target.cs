using UnityEngine;

public class Target : MonoBehaviour
{
    public float timeTaken = 0;

    private void Update()
    {
        timeTaken += Time.deltaTime;
        if (timeTaken >= 5)
        {
            SprayWall_Minigame.instance.score -= 20;
            Destroy(gameObject);
        }
    }

    public void clicked()
    {
        SprayWall_Minigame.instance.destroyedTargetsCount++;
        SprayWall_Minigame.instance.score += 50 / timeTaken;
        Destroy(gameObject);
    }
}
