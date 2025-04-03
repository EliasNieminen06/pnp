using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void PlayB()
    {
        GameManager.instance.Begin();
    }
}
