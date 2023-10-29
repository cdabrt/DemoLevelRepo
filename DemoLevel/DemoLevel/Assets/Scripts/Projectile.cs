using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 20f;
    
    private void Update()
    {
        transform.position += transform.forward * (projectileSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Wait so damage can be applied
        Destroy(gameObject, 0.02f);
    }
}
