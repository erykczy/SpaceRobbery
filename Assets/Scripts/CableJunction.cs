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
            if (device.TryGetComponent<Cable>(out var cable))
            {
                if (cable.InputDevice != Device)
                    continue;
            }
            if (Device.Activators.Contains(device)) continue;
            Device.ActivateAnother(device);
        }
    }

    private void OnDeactivated()
    {
        foreach (var device in Device.AdjacentDevices)
        {
            if (device == null) continue;
            Device.DeactivateAnother(device);
        }
    }
}
