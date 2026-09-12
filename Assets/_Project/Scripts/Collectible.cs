using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip collectClip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(collectClip, transform.position);
            GameManager.Instance.CollectCrystal();
            Destroy(gameObject);
        }
    }
    
}
