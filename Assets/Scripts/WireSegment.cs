using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WireSegment : MonoBehaviour
{
    public DirectionMath.Direction InputDirection;
    public DirectionMath.Direction OutputDirection;
    public float SignalForwardingTime { get; set; }
    public float? SignalForwardingTimeOfWholeWire { get; set; }
    public Device ThisDevice { get; private set; }
    public Tile Tile { get; private set; }
    public Device InputDevice { get; private set; }
    public Device OutputDevice { get; private set; }
    public List<Signal> Signals { get; private set; } = new();

    private void Awake()
    {
        ThisDevice = GetComponent<Device>();
        Tile = GetComponent<Tile>();
        ThisDevice.WhenActivated.AddListener(OnActivated);
        ThisDevice.WhenDeactivated.AddListener(OnDeactivated);
    }

    private void Start()
    {
        InputDevice = FindInputDevice();
        OutputDevice = FindOutputDevice();
        if (!SignalForwardingTimeOfWholeWire.HasValue)
            throw new System.Exception("Signal forwarding time of the whole wire is null!");
    }

    private void Update()
    {
        for(int i = Signals.Count - 1; i >= 0; i--)
        {
            var signal = Signals[i];
            signal.End += Time.deltaTime / SignalForwardingTime;
            if (signal.DetachedFromInput)
                signal.Start += Time.deltaTime / SignalForwardingTime;

            if(signal.Start >= signal.End)
            {
                Signals.RemoveAt(i);

                if (OutputDevice != null)
                    OutputDevice.Deactivate(ThisDevice);

                continue;
            }
            if(signal.End >= 1)
            {
                signal.End = 1f;
                if(OutputDevice != null)
                    OutputDevice.Activate(ThisDevice);
            }
        }
    }

    private void OnActivated() => Signals.Add(new Signal() { DetachedFromInput = false });

    private void OnDeactivated()
    {
        Signals.Last().DetachedFromInput = true;
    }

    private Device FindInputDevice()
    {
        var tile = Tile.Map.GetTile(Tile.Position + DirectionMath.DirectionToIntVector(InputDirection));
        return tile == null ? null : tile.GetComponent<Device>();
    }

    private Device FindOutputDevice()
    {
        var tile = Tile.Map.GetTile(Tile.Position + DirectionMath.DirectionToIntVector(OutputDirection));
        return tile == null ? null : tile.GetComponent<Device>();
    }

    public bool IsStraight()
    {
        if (InputDirection == DirectionMath.Direction.Left && OutputDirection == DirectionMath.Direction.Right) return true;
        if (InputDirection == DirectionMath.Direction.Right && OutputDirection == DirectionMath.Direction.Left) return true;
        if (InputDirection == DirectionMath.Direction.Up && OutputDirection == DirectionMath.Direction.Down) return true;
        if (InputDirection == DirectionMath.Direction.Down && OutputDirection == DirectionMath.Direction.Up) return true;
        return false;
    }

}
