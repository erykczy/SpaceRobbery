using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public float Time;

    private void Awake()
    {
        var cables = GetComponentsInChildren<WireSegment>();
        var timeForEach = Time / cables.Length;
        foreach (var cable in cables)
        {
            cable.SignalForwardingTime = timeForEach;
            cable.SignalForwardingTimeOfWholeWire = Time;
        }
    }
}
