using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RadarVisualization : MonoBehaviour
{
#if UNITY_EDITOR
    private void Update()
    {
        var radar = GetComponentInParent<Radar>();
        var renderer = GetComponent<SpriteRenderer>();

        transform.localScale = radar.Radius * 2f * Vector3.one;

        var block = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(block);
        block.SetFloat("_ViewAngle", radar.ViewAngle);
        renderer.SetPropertyBlock(block);
    }
#endif
}
