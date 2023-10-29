using UnityEngine;

namespace ScriptableObjects.Enemies
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
    public class EnemySo : ScriptableObject
    {
        public float minDistanceFromPlayer = 40;
        public float maxDistanceFromPlayer = 20;
        public float cooldown = 2;
        public float patrolTime = 5;
        public float patrolDistance = 400;
    }
}
