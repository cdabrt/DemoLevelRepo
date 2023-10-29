using System;
using UnityEngine;
using UnityEngine.VFX;

namespace Entities
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] private float health = 100;
        [SerializeField] private VisualEffectAsset hitEffect;
        [SerializeField] private VisualEffectAsset hitEffectStrength;
        
        private void Update() { if (health <= 0) { Destroy(gameObject); } }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("DamageCauser"))
            {
                //if weakness
                health -= other.GetComponent<DamageCauser>().Damage;
                Instantiate(hitEffect, other.transform);
                //else
                //health -= other.GetComponent<DamageCauser>().Damage / 3;
                //Instantiate(hitEffectStrength, other.transform);
            }
        }
    }
}
