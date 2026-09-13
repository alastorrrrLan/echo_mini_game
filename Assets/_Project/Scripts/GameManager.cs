using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Crystal Settings")]
    public int totalCrystals = 3;

    private int collectedCrystals = 0;

    [Header("Exit")]
    public GameObject exitDoor;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EchoController echoController;

    private bool gameOver = false;

    private void Awake()
    {
        Instance = this;
    }

    public void CollectCrystal()
    {
        collectedCrystals++;
        if (collectedCrystals >= totalCrystals)
        {
            OpenExit();
        }
        Debug.Log("Collected Crystals: " + collectedCrystals + "/" + totalCrystals);
    }

    private void OpenExit()
    {
        if (exitDoor != null)
        {
            exitDoor.GetComponent<ExitDoor>().Unlock();
        }
    }

    public void WinGame()
    {
        Debug.Log("You Win!");
        // You can add additional logic here, such as loading the next level or displaying a victory message.
    }

    public void GameOver()
    {
        if (gameOver)
          return;
        
        gameOver = true;
        gameOverPanel.SetActive(true);

        playerController.enabled = false;
        echoController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
