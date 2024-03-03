using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionRenderer : MonoBehaviour
{
    public SpriteRenderer Fill;

    private void Awake()
    {
        Fill.enabled = false;
        var device = GetComponentInParent<Device>();
        device.WhenActivated.AddListener(OnActivated);
        device.WhenDeactivated.AddListener(OnDeactivated);
    }
    
    private void OnActivated() => Fill.enabled = true;
    private void OnDeactivated() => Fill.enabled = false;
}
