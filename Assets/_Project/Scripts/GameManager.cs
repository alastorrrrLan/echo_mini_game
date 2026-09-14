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
    public GameObject exitTrigger;

    [Header("GamePlay")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EchoController echoController;
    [SerializeField] private EnemyController enemyController;

    [Header("UI")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gameOverPanel;

    private bool gameOver = false;

    private bool hasWon = false;

    private static bool skipMainMenu = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (skipMainMenu)
        {
            skipMainMenu = false;
            StartGame();
        }
        else
        {
            ShowMainMenu();
        }
    }

    void ShowMainMenu()
    {
        startPanel.SetActive(true);
        playerController.enabled = false;
        echoController.enabled = false;

        if (enemyController != null)
        {
            enemyController.enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        playerController.enabled = true;
        echoController.enabled = true;

        if (enemyController != null)
        {
            enemyController.enabled = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void CollectCrystal()
    {
        collectedCrystals++;
        if (collectedCrystals >= totalCrystals)
        {
            OpenExit();
            exitTrigger.SetActive(true);
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
        if (hasWon || gameOver)
            return;

        hasWon = true;
        winPanel.SetActive(true);

        playerController.enabled = false;
        echoController.enabled = false;

        if (enemyController != null)
        {
            enemyController.enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GameOver()
    {
        if (gameOver || hasWon)
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
        skipMainMenu = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        skipMainMenu = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
