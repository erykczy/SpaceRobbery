using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Device : MonoBehaviour, IActivator
{
    public UnityEvent<IActivator> WhenActivatorAdded = new();
    public UnityEvent<IActivator> WhenActivatorRemoved = new();
    public UnityEvent WhenActivated = new();
    public UnityEvent WhenDeactivated = new();
    public bool Activated { get; private set; } = false;
    public bool OutputActivated { get; private set; } = false;
    public List<IActivator> Activators { get; private set; } = new();
    public List<Wire> OutputWires { get; private set; } = new();
    public Tile Tile { get; private set; }

    private void Awake()
    {
        Tile = GetComponent<Tile>();
    }

    private void Start()
    {
        OutputWires = FindOutputWires();
    }

    private void OnDestroy()
    {
        DeactivateOutput();
    }

    public void ActivateOutput()
    {
        if (OutputActivated) return;
        OutputActivated = true;

        foreach (var wire in OutputWires)
        {
            if(wire == null)
            {
                OutputWires.Remove(wire);
                continue;
            }
            wire.StartSignal(this);
        }
    }

    public void DeactivateOutput()
    {
        if (!OutputActivated) return;
        OutputActivated = false;

        foreach (var wire in OutputWires)
        {
            if (wire == null)
            {
                OutputWires.Remove(wire);
                continue;
            }
            wire.DetachSignal(this);
        }
    }

    public void Deactivate(IActivator deactivator)
    {
        if (!Activators.Contains(deactivator)) return;
        Activators.Remove(deactivator);
        WhenActivatorRemoved.Invoke(deactivator);
        if (!Activated) return;
        Activated = Activators.Count != 0;
        if(!Activated) WhenDeactivated.Invoke();
    }

    public void Activate(IActivator activator)
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

    private List<Wire> FindOutputWires()
    {
        var list = new List<Wire>();
        var adjacentTiles = Tile.Map.GetSideAdjacentTiles(Tile.Position);

        foreach (var tile in adjacentTiles)
        {
            if (tile.TryGetComponent<Cable>(out var cable))
            {
                if (cable.GetInputTile() == Tile && !list.Contains(cable.Wire))
                    list.Add(cable.Wire);
            }
        }
        return list;
    }
}
