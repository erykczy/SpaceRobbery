using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public DirectionMath.Direction InputDirection;
    public DirectionMath.Direction OutputDirection;
    public float SignalForwardingTime { get; set; }
    public float? SignalForwardingTimeOfWholeWire { get; set; }
    public Device Device { get; private set; }
    public Tile Tile { get; private set; }
    public Device InputDevice { get; private set; }
    public Device OutputDevice { get; private set; }
    public float SignalForwardingProgress { get; private set; } = 0f;
    private bool singalForwardingInProgress = false;

    private void Awake()
    {
        Device = GetComponent<Device>();
        Tile = GetComponent<Tile>();
        Device.WhenActivated.AddListener(OnActivated);
        Device.WhenDeactivated.AddListener(OnDeactivated);
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
        if(singalForwardingInProgress)
        {
            SignalForwardingProgress += Time.deltaTime;

            if(SignalForwardingProgress >= SignalForwardingTime)
            {
                singalForwardingInProgress = false;
                SignalForwardingProgress = SignalForwardingTime;
                if (OutputDevice != null)
                    Device.ActivateAnother(OutputDevice);
            }
        }
    }

    private void OnActivated() => singalForwardingInProgress = true;

    private void OnDeactivated()
    {
        //singalForwardingInProgress = false;
        //SignalForwardingProgress = 0f;
        //ForwardDeactivation();
    }

    private void ForwardDeactivation()
    {
        if (OutputDevice == null) return;
        Device.DeactivateAnother(OutputDevice);
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

    /*
    private float FindSignalForwardingTimeOfWholeWire()
    {
        float time = 0f;

        // pre
        var checkedDevice = InputDevice;
        while(checkedDevice.TryGetComponent<Cable>(out var cable))
        {
            time += cable.SignalForwardingTime;
            checkedDevice = cable.InputDevice;
        }

        // post
        checkedDevice = OutputDevice;
        while (checkedDevice.TryGetComponent<Cable>(out var cable))
        {
            time += cable.SignalForwardingTime;
            checkedDevice = cable.OutputDevice;
        }

        return time;
    }
    */
}
