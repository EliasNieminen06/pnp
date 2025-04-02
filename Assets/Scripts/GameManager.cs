using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        Menu,
        Game,
        MiniGame
    }

    public State currentState = State.Menu;

    public static GameManager instance;
    public int spray1Need = 0;
    public int spray2Need = 0;
    public int spray1Amount = 0;
    public int spray2Amount = 0;

    public float totalScore;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (spray1Amount == spray1Need &&  spray2Amount == spray2Need)
        {
            Win();
        }
        switch (currentState)
        {
            case State.Menu:
                break;

            case State.Game:
                break;

            case State.MiniGame:
                break;
        }
    }

    public void Begin()
    {
        SceneManager.LoadScene("GameScene");
        currentState = State.Game;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Win()
    {
        SceneManager.LoadScene("WinMenu");
        currentState = State.Menu;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //PlayerPrefs.Save(totalScore, "");
    }

    public void Caught()
    {
        SceneManager.LoadScene("CaughtMenu");
        currentState = State.Menu;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
