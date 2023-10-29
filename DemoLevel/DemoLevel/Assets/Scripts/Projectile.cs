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
    
    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
