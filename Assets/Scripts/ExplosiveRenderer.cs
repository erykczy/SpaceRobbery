using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveRenderer : MonoBehaviour
{
    public GameObject ExplosionEffect;

    private void Awake()
    {
        GetComponent<Explosive>().WhenExploded.AddListener(OnExploded);
    }

    private void OnExploded()
    {
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
    }
}
