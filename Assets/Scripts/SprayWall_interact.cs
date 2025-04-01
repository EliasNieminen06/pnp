using UnityEngine;

public class SprayWall_interact : MonoBehaviour, IInteractable
{
    public GameObject mingame;

    public void Interact()
    {
        mingame.GetComponent<SprayWall_Minigame>().StartGame();
    }
}
