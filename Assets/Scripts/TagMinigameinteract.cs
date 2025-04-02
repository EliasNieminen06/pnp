using UnityEngine;

public class TagMinigameinteract : MonoBehaviour, IInteractable
{
    public GameObject mingame;
    public bool played;
    public bool isTag;

    public void Interact()
    {
        if (!played)
        {
            mingame.GetComponent<Minigame>().StartGame(isTag);
            GameManager.instance.currentState = GameManager.State.MiniGame;
        }
    }
}
