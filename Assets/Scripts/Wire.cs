using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wire : MonoBehaviour, IActivator
{
    [Tooltip("The total time it takes for a signal to travel through this wire")]
    public float SignalTime;
    public Dictionary<IActivator, Signal> Signals = new();
    public Device InputDevice { get; private set; }
    public Device OutputDevice { get; private set; }

    private void Awake()
    {
        FindInputAndOutputDevice(out Device inputDevice, out Device outputDevice);
        InputDevice = inputDevice;
        OutputDevice = outputDevice;
    }

    private void Update()
    {
        var signals = Signals.Values.ToList();
        for(int i = signals.Count; i >= 0; i--)
        {
            var signal = signals[i];
            signal.Start += SignalTime * Time.deltaTime;
            if (signal.DetachedFromOutput)
                signal.End += SignalTime * Time.deltaTime;

            if (signal.End >= 1f)
            {
                signal.End = 1f;
                OutputDevice.Activate(this);
            }
            if (signal.Start >= signal.End)
            {
                signals.RemoveAt(i);
                OutputDevice.Deactivate(this);
            }
        }
    }

    public void OnCableDestroyed()
    {
        Destroy(this);
    }

    private void FindInputAndOutputDevice(out Device input, out Device output)
    {
        input = null;
        output = null;
        var cables = GetComponentsInChildren<Cable>();
        foreach (var cable in cables)
        {
            if (input == null) cable.GetInputTile().TryGetComponent(out input);
            if (output == null) cable.GetOutputTile().TryGetComponent(out output);
        }
    }

    public void StartSignal(IActivator activator)
    {
        var signal = new Signal();
        Signals.Add(activator, signal);
    }

    public void DetachSignal(IActivator activator)
    {
        Signals[activator].DetachedFromOutput = true;
    }
}
