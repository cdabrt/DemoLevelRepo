using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons
{
    public class Revolver : Weapon
    {
        //To ScriptableObject
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float maxAmmo = 50;
        private float _currentAmmo;
        [SerializeField] private Transform shootPoint;

        private new void Start() { 
            _currentAmmo = maxAmmo;
            base.Start();
        }

        public override void Attack()
        {
            //Set direction to center of screen
            var bullet = Instantiate(bulletPrefab, shootPoint);
            bullet.transform.parent = null;
        }
    }
}
