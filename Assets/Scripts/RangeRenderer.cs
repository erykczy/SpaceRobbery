using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete("To remove")]
[ExecuteInEditMode]
public class RangeRenderer : MonoBehaviour
{
    public bool Horizontal = true;

    [Range(0f, 1f)]
    public float Fill;
    public Transform Start;
    public Transform End;

    public SpriteRenderer SpriteRenderer { get; private set; }

    private void Awake()
    {
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if(Horizontal)
        {
            var totalSignedSize = End.position.x - Start.position.x;
            var mySignedSize = totalSignedSize * Fill;

            transform.localScale = new Vector3(Mathf.Abs(mySignedSize), transform.localScale.y);
            transform.position = Start.position + new Vector3(mySignedSize / 2f, 0);
        }
        else
        {
            var totalSignedSize = End.position.y - Start.position.y;
            var mySignedSize = totalSignedSize * Fill;

            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(mySignedSize));
            transform.position = Start.position + new Vector3(0, mySignedSize / 2f);
        }
    }
}
