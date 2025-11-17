using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] public Transform[] walkingPath;
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
            currentIndex = (currentIndex + 1) % walkingPath.Length;
            agent.SetDestination(walkingPath[currentIndex].position);
        }

    }

}
