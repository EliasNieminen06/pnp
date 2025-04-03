using UnityEngine;

public class TagMinigameinteract : MonoBehaviour, IInteractable
{
    public GameObject mingame;
    public bool played;
    public bool isTag;
    public Canvas painting;

    private void Start()
    {
        played = false;
        painting.enabled = false;
    }

    public void Interact()
    {
        if (!played)
        {
            mingame.GetComponent<Minigame>().StartGame(isTag, painting, this);
            GameManager.instance.currentState = GameManager.State.MiniGame;
        }
    }
}
