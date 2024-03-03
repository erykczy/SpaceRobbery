using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CableTooltip : MonoBehaviour
{
    public LayerMask TilesLayer;
    public Transform Content;
    public TextMeshProUGUI Text;

    private void Update()
    {
        var time = GetSignalForwardingTimeOfHoveredWire();
        if(!time.HasValue)
        {
            Content.gameObject.SetActive(false);
            return;
        }

        Text.text = time + "s";
        Content.gameObject.SetActive(true);
    }

    public float? GetSignalForwardingTimeOfHoveredWire()
    {
        var collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition), TilesLayer);
        if (collider == null) return null;
        var cable = collider.GetComponentInParent<Cable>();
        if (cable == null) return null;
        return cable.SignalForwardingTimeOfWholeWire;
    }
}
