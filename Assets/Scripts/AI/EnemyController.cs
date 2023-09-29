using System.Collections;
using FishNet.Object;
using FishNet.Object.Synchronizing;
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

        private NavMeshAgent navMeshAgent;
        // attribute used to check if the target is reachable
        NavMeshPath navMeshPath;
        [SyncVar]
        public EnemyState State = EnemyState.Idle;
        [SerializeField]
        private float patrolSpeed = 1f;
        [SerializeField]
        private float patrolAngularSpeed = 40f;
        [SerializeField]
        private float chaseSpeed = 4f;
        [SerializeField]
        private float chaseAngularSpeed = 120f;

        [SerializeField]
        private LayerMask playerLayerMask;
        [SerializeField]
        private float chaseRadius = 10f;
        private GameObject playerChased;

        public override void OnStartServer()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshPath = new NavMeshPath();
        }

        void Update()
        {
            if(!base.IsServer) return;

            switch(State)
            {
                case EnemyState.Idle:
                    State = EnemyState.Patrol;
                    break;
                case EnemyState.Sit:
                    // If we have no player to chase, find a new player to chase
                    if (playerChased == null)
                    {
                        FindNewPlayerToChase();
                    }
                    break;
                case EnemyState.Patrol:
                    // If we have no player to chase, find a new player to chase
                    if (playerChased == null)
                    {
                        FindNewPlayerToChase();
                    }
                    if (navMeshAgent.destination == null || (Vector3.Distance(transform.position, navMeshAgent.destination) < 2f))
                    {
                        if (Random.Range(0, 100) < 50)
                        {
                            SetupAIToPatrol();
                        }
                        else
                        {
                            State = EnemyState.Sit;
                            StartCoroutine(Sit());
                        }
                    }
                    break;
                case EnemyState.Chase:
                    // If we have a player to chase and we can calculate a path to it, chase it
                    if (playerChased != null && navMeshAgent.CalculatePath(playerChased.transform.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
                    {
                        currentWaypoint = playerChased.transform.position;
                        navMeshAgent.SetDestination(currentWaypoint);
                    } else
                    {
                        FindNewPlayerToChase();
                    }
                    break;
                case EnemyState.Attack:
                    break;
            }
        }

        IEnumerator Sit()
        {
            navMeshAgent.isStopped = true;
            yield return new WaitForSeconds(Random.Range(10f, 40f));
            navMeshAgent.isStopped = false;
            SetupAIToPatrol();
        }

        private void FindNewPlayerToChase() {
            Debug.Log("Bear n. " + gameObject.GetInstanceID() + " is looking for a player...");
            Collider[] colliders = Physics.OverlapSphere(transform.position, chaseRadius, playerLayerMask);
            if (colliders.Length > 0 && navMeshAgent.CalculatePath(colliders[0].gameObject.transform.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                SetupAIToChase(colliders[0].gameObject);
            } else if (State == EnemyState.Chase)
            {
                SetupAIToPatrol();
            }
        }

        private void SetupAIToPatrol()
        {
            StopCoroutine(Sit());
            playerChased = null;
            navMeshAgent.speed = patrolSpeed;
            navMeshAgent.angularSpeed = patrolAngularSpeed;
            State = EnemyState.Patrol;
            currentWaypoint = GetRandomPositionInPieSlice();
            navMeshAgent.SetDestination(currentWaypoint);
            navMeshAgent.isStopped = false;
        }

        private void SetupAIToChase(GameObject _chasedPlayer)
        {
            if(_chasedPlayer == null)
            {
                Debug.LogError("Trying to chase a null player");
                return;
            }
            // If we can't calculate a path to the player, don't chase it
            if(!navMeshAgent.CalculatePath(_chasedPlayer.transform.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                return;
            }
            StopCoroutine(Sit());
            playerChased = _chasedPlayer;
            navMeshAgent.speed = chaseSpeed;
            navMeshAgent.angularSpeed = chaseAngularSpeed;
            State = EnemyState.Chase;
            navMeshAgent.SetDestination(playerChased.transform.position);
            navMeshAgent.isStopped = false;
        }

        private void OnDrawGizmosSelected()
        {
            if (!DrawGizmos) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseRadius);
        }
    }
}
