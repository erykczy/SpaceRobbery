using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Explosive : MonoBehaviour
{
    public LayerMask TilesLayer;
    public float ExplosionRadius;
    public UnityEvent WhenExploded = new();

    private void Awake()
    {
        GetComponent<Device>().WhenActivated.AddListener(OnActivated);
    }

    private void OnActivated()
    {
        Explode();
    }

    private void Explode()
    {
        WhenExploded.Invoke();
        var colliders = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius, TilesLayer);
        foreach (var collider in colliders)
        {
            var tile = collider.GetComponentInParent<Tile>();
            Destroy(tile.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
