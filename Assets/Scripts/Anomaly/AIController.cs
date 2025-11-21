using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] public Transform[] walkingPath;
    [SerializeField] public Transform initialPosition;
    [SerializeField] public bool stopMoving = true;
    [SerializeField] private float Speed = 3.5f;


    public float arriveDistance = 1f;

    private NavMeshAgent agent;
    private int currentIndex = 0;
    private Animator animator;
    private Vector3 startingPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.speed = Speed;
        
        if (initialPosition == null)
        {
            startingPosition = transform.position;
        }

        StartWalkingPath();
    }

    void Update()
    {
        if(gameObject.tag == "Anomaly"){
            HitAnomaly();
        }

        animator.SetBool("isMoving", true);

        if (stopMoving == true) StopMoving();
        MoveConstantly();
        
    }

    private void MoveConstantly()
    {
        if (walkingPath.Length == 0) return;
        if (!agent.pathPending && agent.remainingDistance < arriveDistance)
        {
            currentIndex = (currentIndex + 1) % walkingPath.Length; 
            agent.SetDestination(walkingPath[currentIndex].position);
        }   
    }

    private void StopMoving()
    {
        if (walkingPath.Length == 0) return;
        animator.SetBool("isMoving", !agent.isStopped && agent.velocity.magnitude > 0.1f);
        if (!agent.pathPending && agent.remainingDistance < arriveDistance)
        {
            if (currentIndex < walkingPath.Length - 1)
            {
                currentIndex++;
                agent.SetDestination(walkingPath[currentIndex].position);
            }
            else
            {
                animator.SetBool("isMoving", false);
                agent.isStopped = true;
            }
        }

    }

    private void HitAnomaly()
    {
        Debug.Log("I am: " + gameObject.tag);

    }

    private void OnEnable()
    {
        GameManager.OnPlayerGuess += ResetNPCs;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerGuess -= ResetNPCs;
    }

    private void ResetNPCs()
    {
        Vector3 resetPosition = initialPosition != null ? initialPosition.position : startingPosition;
        transform.position = resetPosition;
        currentIndex = 0;
        agent.isStopped = false;
        StartWalkingPath();
    }
    
    private void StartWalkingPath()
    {
        if (walkingPath.Length > 0)
        {
            currentIndex = 0;
            agent.SetDestination(walkingPath[currentIndex].position);
            animator.SetBool("isMoving", true);
        }
    }
}
