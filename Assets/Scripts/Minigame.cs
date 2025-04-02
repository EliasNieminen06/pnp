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

    private void Awake()
    {
        instance = this;
    }

    public void StartGame(bool isTag)
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
        StartCoroutine(MiniGame(isTag));
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

    void FinishGame(bool isTag)
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
    }

    public IEnumerator MiniGame(bool isTag)
    {
        while (destroyedTargetsCount < targetCount)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnTarget();
        }

        FinishGame(isTag);
    }
}
