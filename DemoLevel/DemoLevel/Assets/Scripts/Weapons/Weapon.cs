using System;
using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        protected void Start()
        {
            transform.tag = "Weapon";
        }

        public virtual void Attack() {}
    }
}
