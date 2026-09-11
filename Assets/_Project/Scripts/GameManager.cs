using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Crystal Settings")]
    public int totalCrystals = 3;

    private int collectedCrystals = 0;

    [Header("Exit")]
    public GameObject exitDoor;
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
            exitDoor.SetActive(false);
        }
    }

    public void WinGame()
    {
        Debug.Log("You Win!");
        // You can add additional logic here, such as loading the next level or displaying a victory message.
    }
}
