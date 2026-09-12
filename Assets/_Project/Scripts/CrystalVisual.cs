using UnityEngine;

public class CrystalVisual : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 40f;
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float floatAmount = 0.15f;

    private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        float yOffset = Mathf.Sin(floatSpeed * Time.time) * floatAmount;
        transform.position = startPosition + Vector3.up * yOffset;
    }
}
