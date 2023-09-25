using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace HTNWIC.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : AIController
    {

        public enum EnemyState
        {
            Idle,
            Sit,
            Patrol,
            Chase,
            Attack
        }

        private NavMeshAgent agent;
        public EnemyState State { get; private set; } = EnemyState.Idle;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            currentWaypoint = GetRandomPositionInPieSlice();
            agent.SetDestination(currentWaypoint);
            State = EnemyState.Patrol;
        }

        void Update()
        {
            if (Vector3.Distance(transform.position, agent.destination) < 2f && State == EnemyState.Patrol)
            {
                if (Random.Range(0, 100) < 90)
                {
                    State = EnemyState.Patrol;
                }
                else
                {
                    State = EnemyState.Sit;
                }

                switch (State)
                {
                    case EnemyState.Sit:
                        StartCoroutine(Sit());
                        break;
                    case EnemyState.Patrol:
                        currentWaypoint = GetRandomPositionInPieSlice();
                        agent.SetDestination(currentWaypoint);
                        break;
                }
            }
        }

        IEnumerator Sit()
        {
            agent.isStopped = true;
            yield return new WaitForSeconds(10);
            agent.isStopped = false;
            currentWaypoint = GetRandomPositionInPieSlice();
            agent.SetDestination(currentWaypoint);
            State = EnemyState.Patrol;
        }
    }
}
