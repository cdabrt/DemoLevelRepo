using System;
using UnityEngine;
using UnityEngine.VFX;

public class DamageCauser : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private VisualEffectAsset vfx;

    private void Start()
    {
        transform.tag = "DamageCauser";
    }

    private void OnDestroy() { if (vfx != null) { PlayVFX(); } }

    private void PlayVFX() { Instantiate(vfx, transform); }

    public float Damage => damage;
}
