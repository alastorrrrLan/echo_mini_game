using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float arrivalDistance = 0.2f;

    [Header("References")]
    [SerializeField] private GameManager gameManager;

    private int currentPointIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.GameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
{
    if (patrolPoints.Length == 0)
        return;

    Transform targetPoint =
        patrolPoints[currentPointIndex];

    Vector3 targetPosition = targetPoint.position;

    targetPosition.y = transform.position.y;

    transform.position =
        Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

    Vector3 direction =
        targetPosition - transform.position;

    if (direction != Vector3.zero)
    {
        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                5f * Time.deltaTime
            );
    }

    float distance =
        Vector3.Distance(
            transform.position,
            targetPosition
        );

    if (distance <= arrivalDistance)
    {
        currentPointIndex++;

        if (currentPointIndex >= patrolPoints.Length)
        {
            currentPointIndex = 0;
        }
    }
}
}
