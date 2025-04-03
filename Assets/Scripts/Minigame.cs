using NUnit.Framework.Constraints;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    public static Minigame instance;
    public GameObject targetPrefab;
    public float spawnInterval = 1f;
    public Vector2 spawnArea = new Vector2(800, 600);
    public int tagTargetCount;
    public int sprayTargetCount;
    public Canvas minigameCanvas;

    public int targetCount;

    public int destroyedTargetsCount;

    public float score;

    public AudioSource spraySound;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            failGame();
        }
    }

    public void StartGame(bool isTag, Canvas painting, TagMinigameinteract interact)
    {
        if (isTag)
        {
            targetCount = tagTargetCount;
        }
        else
        {
            targetCount = sprayTargetCount;
        }
        destroyedTargetsCount = 0;
        Player.instance.canMove = false;
        minigameCanvas.enabled = true;
        StartCoroutine(MiniGame(isTag, painting, interact));
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void SpawnTarget()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(0, spawnArea.x),
            Random.Range(0, spawnArea.y),
            0);

        Instantiate(targetPrefab, spawnPosition, Quaternion.identity, transform);
    }

    void FinishGame(bool isTag, Canvas painting, TagMinigameinteract interact)
    {
        GameManager.instance.currentState = GameManager.State.Game;
        minigameCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Player.instance.canMove = true;
        GameManager.instance.totalScore += score;
        score = 0;
        if (isTag)
        {
            GameManager.instance.spray1Amount++;
        }
        else
        {
            GameManager.instance.spray2Amount++;
        }
        painting.enabled = true;
        spraySound.Play();
        interact.played = true;
    }

    void failGame()
    {
        StopAllCoroutines();
        GameManager.instance.currentState = GameManager.State.Game;
        minigameCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Player.instance.canMove = true;
        score = 0;
    }

    public IEnumerator MiniGame(bool isTag, Canvas painting, TagMinigameinteract interact)
    {
        while (destroyedTargetsCount < targetCount)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnTarget();
        }

        FinishGame(isTag, painting, interact);
    }
}
