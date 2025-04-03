using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FieldOfSight : MonoBehaviour
{
    public enum State
    {
        Wandering,
        FollowingPlayer,
        MiniGame
    }

    public float viewAngle = 90f;
    public float viewDistance = 10f;
    public Transform player;
    public NavMeshAgent agent;

    public float wanderRadius = 10f;

    [SerializeField] private State currentState = State.Wandering;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Wandering:
                agent.speed = 2;
                Wander();
                ScanForPlayer();
                break;

            case State.FollowingPlayer:
                agent.speed = 4;
                FollowPlayer();
                break;

            case State.MiniGame:
                agent.speed = 0;
                break;
        }
    }

    void Wander()
    {
        if (agent.remainingDistance < 1f)
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
    }

    void ScanForPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        if (angle < viewAngle / 2)
        {
            if (Vector3.Distance(transform.position, player.position) < viewDistance)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, viewDistance))
                {
                    if (hit.transform == player)
                    {
                        currentState = State.FollowingPlayer;
                    }
                }
            }
        }
    }

    private void FollowPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        if (angle < viewAngle / 2)
        {
            if (Vector3.Distance(transform.position, player.position) < viewDistance)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, viewDistance))
                {
                    if (hit.transform == player)
                    {
                        agent.SetDestination(player.position);
                    }
                    else
                    {
                        currentState = State.Wandering;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.Caught();
        }
    }
}
