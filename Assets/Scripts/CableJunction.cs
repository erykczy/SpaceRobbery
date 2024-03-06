using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableJunction : MonoBehaviour
{
    public List<Device> AdjacentDevices = new();
    public Device ThisDevice { get; private set; }
    public Tile Tile { get; private set; }

    private void Awake()
    {
        Tile = GetComponent<Tile>();
        ThisDevice = GetComponent<Device>();
        ThisDevice.WhenActivated.AddListener(OnActivated);
        ThisDevice.WhenDeactivated.AddListener(OnDeactivated);
    }

    private void Start()
    {
        AdjacentDevices = FindAdjacentDevices();
    }

    private void OnActivated()
    {
        ThisDevice.ActivateOutput();
        foreach (var device in AdjacentDevices)
        {
            if (device == null)
            {
                AdjacentDevices.Remove(device);
                continue;
            }
            if (ThisDevice.Activators.Contains(device)) continue;
            device.Activate(ThisDevice);
        }
    }

    private void OnDeactivated()
    {
        ThisDevice.DeactivateOutput();
        foreach (var device in AdjacentDevices)
        {
            if (device == null)
            {
                AdjacentDevices.Remove(device);
                continue;
            }
            device.Deactivate(ThisDevice);
        }
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
