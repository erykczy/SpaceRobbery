using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveRangeVisualizer : MonoBehaviour
{
    public Explosive Explosive { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    private void Awake()
    {
        Explosive = GetComponentInParent<Explosive>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        SpriteRenderer.transform.localScale = Explosive.ExplosionRadius * 2f * Vector2.one ;
    }

    private void OnMouseEnter()
    {
        SpriteRenderer.enabled = true;
    }

    private void OnMouseExit()
    {
        SpriteRenderer.enabled = false;
    }
}
