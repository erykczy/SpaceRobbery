using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableJunction : MonoBehaviour
{
    public Device Device { get; private set; }

    private void Awake()
    {
        Device = GetComponent<Device>();
        Device.WhenActivated.AddListener(OnActivated);
        Device.WhenDeactivated.AddListener(OnDeactivated);
    }

    private void OnActivated()
    {
        foreach (var device in Device.AdjacentDevices)
        {
            if(device == null) continue;
            if (device.TryGetComponent<WireSegment>(out var cable))
            {
                if (cable.InputDevice != Device)
                    continue;
            }
            if (Device.Activators.Contains(device)) continue;
            device.Activate(Device);
        }
    }

    private void OnDeactivated()
    {
        foreach (var device in Device.AdjacentDevices)
        {
            if (device == null) continue;
            device.Deactivate(Device);
        }
    }
}
