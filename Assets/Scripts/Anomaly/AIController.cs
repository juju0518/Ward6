using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] public Transform[] walkingPath;
    [SerializeField] public Transform initialPosition;
    public float arriveDistance = 1f;

    private NavMeshAgent agent;
    private int currentIndex = 0;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (walkingPath.Length > 0)
        {
            agent.SetDestination(walkingPath[currentIndex].position);
        }
    }

    void Update()
    {
        animator.SetBool("isMoving", true);

        if (walkingPath.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < arriveDistance)
        {
            if (currentIndex < walkingPath.Length - 1){
                currentIndex++;
                agent.SetDestination(walkingPath[currentIndex].position);
            }
            else
            {
                animator.SetBool("isMoving", false);
                agent.isStopped = true;
            }
        }

        Debug.Log(gameObject.tag)
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
        agent.SetDestination(initialPosition.position);
    }
 
}
