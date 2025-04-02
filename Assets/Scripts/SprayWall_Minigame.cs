using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class SprayWall_Minigame : MonoBehaviour
{
    public static SprayWall_Minigame instance;
    public GameObject targetPrefab;
    public float spawnInterval = 1f;
    public Vector2 spawnArea = new Vector2(800, 600);
    public int targetCount;
    public Canvas minigameCanvas;

    public int destroyedTargetsCount;

    public float score;

    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        destroyedTargetsCount = 0;
        FPSController.instance.canMove = false;
        minigameCanvas.enabled = true;
        StartCoroutine(MiniGame());
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

    void FinishGame()
    {
        GameManager.instance.currentState = GameManager.State.Game;
        minigameCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FPSController.instance.canMove = true;
    }

    public IEnumerator MiniGame()
    {
        while (destroyedTargetsCount < targetCount)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnTarget();
        }

        FinishGame();
    }
}
