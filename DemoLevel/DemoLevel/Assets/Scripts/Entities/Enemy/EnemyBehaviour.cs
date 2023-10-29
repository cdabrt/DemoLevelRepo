using System.Collections;
using ScriptableObjects;
using ScriptableObjects.Enemies;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Entities.Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private EnemySo enemySo;
        private NavMeshAgent _agent;
        private Vector3 _randomLocation;
        private float _distance;
        private bool _attackInProgress;
        private bool _patrolInProgress;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _attackInProgress = false;
            _patrolInProgress = true;
            _randomLocation = GenerateRandomLocation();
        }

        private void Update()
        {
            //Has to be calculated in almost every state
            _distance = Vector3.Distance(transform.position, player.position);
        }


        public void OnPatrolEnter()
        {
            _patrolInProgress = true;
            _randomLocation = GenerateRandomLocation();
        }
        
        public void OnPatrolUpdate()
        {
            print("Test");
            //Move back and forth
            if (_patrolInProgress)
            {
                _agent.SetDestination(_randomLocation);

                if (transform.position == new Vector3(_randomLocation.x, transform.position.y, _randomLocation.z))
                {
                    ResetPath();
                    _patrolInProgress = false;
                }
            }
            else
            {
                _randomLocation = GenerateRandomLocation();
                _patrolInProgress = true;
            }

            if (_distance <= enemySo.minDistanceFromPlayer) { CustomEvent.Trigger(gameObject,"Follow"); }
        }
        
        public void OnPatrolExit()
        {
            ResetPath();
        }
        
        
        
        public void OnFollowEnter()
        {
            //
        }
        
        public void OnFollowUpdate()
        {
            if (_distance <= enemySo.maxDistanceFromPlayer)
            {
                CustomEvent.Trigger(gameObject,"Attack");
            } else if (_distance > enemySo.minDistanceFromPlayer)
            {
                CustomEvent.Trigger(gameObject,"Patrol");
            }
            else
            {
                //Follow player
                _agent.SetDestination(player.position);
            }
        }
        
        public void OnFollowExit()
        {
            ResetPath();
        }
            
           
        
        public void OnAttackEnter()
        {
            //
        }
        
        public void OnAttackUpdate()
        {
            if (_distance > enemySo.maxDistanceFromPlayer && !_attackInProgress)
            {
                CustomEvent.Trigger(gameObject,"Follow");
            } else if (!_attackInProgress)
            {
                StartCoroutine(Attack());
            }
        }
        
        public void OnAttackExit()
        {
            //
        }

        private IEnumerator Attack()
        {
            _attackInProgress = true;
            print("Attack!");
            yield return new WaitForSeconds(enemySo.cooldown);
            _attackInProgress = false;
        }

        
        
        private Vector3 GenerateRandomLocation()
        {
            var randomDirection = Random.insideUnitSphere * enemySo.patrolDistance;
            randomDirection += transform.position;
            NavMesh.SamplePosition(randomDirection, out var navmeshHit, enemySo.patrolDistance, NavMesh.AllAreas);
            return navmeshHit.position;
        }
        
        private void ResetPath()
        {
            //Check if agent is active and on a navmesh. Prevents errors from arising when closing the game
            if (_agent.isOnNavMesh)
            {
                _agent.ResetPath();
            }
        }
    }
}
