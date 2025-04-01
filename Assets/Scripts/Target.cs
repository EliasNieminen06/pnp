using UnityEngine;

public class Target : MonoBehaviour
{
    public void clicked()
    {
        SprayWall_Minigame.instance.destroyedTargetsCount++;
        Destroy(gameObject);
    }
}
