using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Device : MonoBehaviour
{
    public UnityEvent<Device> WhenActivatorAdded = new();
    public UnityEvent<Device> WhenActivatorRemoved = new();
    public UnityEvent WhenActivated = new();
    public UnityEvent WhenDeactivated = new();
    public bool Activated { get; private set; } = false;
    public List<Device> Activators { get; private set; } = new();
    public List<Device> OutputDevices { get; private set; } = new();
    public List<Device> AdjacentDevices { get; private set; } = new();
    public Tile Tile { get; private set; }

    private void Awake()
    {
        Tile = GetComponent<Tile>();
    }

    private void Start()
    {
        AdjacentDevices = FindAdjacentDevices();
        OutputDevices = FindConnectedDevices();
    }

    private void OnDestroy()
    {
        DeactivateOutputDevices();

    }

    public void ActivateOutputDevices()
    {
        foreach (var device in OutputDevices)
        {
            if (device == null) continue;
            ActivateAnother(device);
        }
    }

    public void DeactivateOutputDevices()
    {
        foreach (var device in OutputDevices)
        {
            if (device == null) continue;
            DeactivateAnother(device);
        }
    }

    public void DeactivateAnother(Device device)
    {
        device.DeativateSelf(this);
    }

    public void ActivateAnother(Device device)
    {
        device.ActivateSelf(this);
    }

    public void DeativateSelf(Device deactivator)
    {
        if (!Activators.Contains(deactivator)) return;
        Activators.Remove(deactivator);
        WhenActivatorRemoved.Invoke(deactivator);
        if (!Activated) return;
        Activated = Activators.Count != 0;
        if(!Activated) WhenDeactivated.Invoke();
    }

    public void ActivateSelf(Device activator)
    {
        if (Activators.Contains(activator)) return;
        Activators.Add(activator);
        WhenActivatorAdded.Invoke(activator);
        if (!Activated)
        {
            Activated = true;
            WhenActivated.Invoke();
        }
    }

    private List<Device> FindConnectedDevices()
    {
        var list = new List<Device>();
        foreach (var device in AdjacentDevices)
        {
            if(device == null) continue;
            if(device.TryGetComponent<Cable>(out var cable))
            {
                if(cable.InputDevice == this)
                    list.Add(device);
            }
            else if(device.TryGetComponent<CableJunction>(out var _))
            {
                list.Add(device);
            }
        }
        return list;
    }

    private List<Device> FindAdjacentDevices()
    {
        var list = new List<Device>();
        var adjacentTiles = Tile.Map.GetSideAdjacentTiles(Tile.Position);
        foreach (var tile in adjacentTiles)
        {
            if (tile.TryGetComponent<Device>(out var device))
            {
                list.Add(device);
            }
        }
        return list;
    }
}
