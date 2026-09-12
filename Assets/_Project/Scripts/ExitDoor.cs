using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Renderer doorRenderer;
    [SerializeField] private Material lockedMaterial;
    [SerializeField] private Material unlockedMaterial;
    private bool isUnlocked = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorRenderer.material = lockedMaterial;
    }

    public void Unlock()
    {
        isUnlocked = true;
        doorRenderer.material = unlockedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
