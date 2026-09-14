using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float arrivalDistance = 0.2f;

    [Header("References")]
    [SerializeField] private GameManager gameManager;

    [Header("Echo Detection")]
    [SerializeField] private float echoHearingRange = 12f;
    [SerializeField] private float investigateSpeed = 3f;
    [SerializeField] private float investigateWaitTime = 3f;

    private int currentPointIndex = 0;
    private bool isInvestigating = false;
    private Vector3 investigatePosition;
    private float investigateTimer = 0f;
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
        if (isInvestigating)
        {
            InvestigateEcho();
        }
        else
        {
            Patrol();
        }
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

    public void HearEcho(Vector3 echoPosition)
    {
        float distanceToEcho = Vector3.Distance(transform.position, echoPosition);
        if (distanceToEcho > echoHearingRange) return;
    
        isInvestigating = true;
        investigatePosition = echoPosition;
        investigateTimer = 0f;
    }

    void InvestigateEcho()
    {
        Vector3 targetPosition = investigatePosition;
        targetPosition.y = transform.position.y;

        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance > arrivalDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                investigateSpeed * Time.deltaTime
            );
            Vector3 direction = targetPosition - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    5f * Time.deltaTime
                );
            }
        }
         else
        {
            investigateTimer += Time.deltaTime;
            if (investigateTimer >= investigateWaitTime)
            {
                isInvestigating = false;
            }
        }
    }


}
