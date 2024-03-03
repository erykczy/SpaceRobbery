using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public LayerMask UnitsLayer;
    public float ViewAngle = 90f;
    public float Radius;
    public float RotationSpeed;
    public Transform Anchor;
    public Device Device { get; private set; }
    public List<Unit> DetectedUnits { get; private set; } = new();

    private void Awake()
    {
        Device = GetComponent<Device>();
    }

    private void Update()
    {
        Anchor.eulerAngles = new Vector3(0, 0, Anchor.eulerAngles.z + Time.deltaTime * RotationSpeed);

        var colliders = Physics2D.OverlapCircleAll(Anchor.position, Radius, UnitsLayer);
        DetectedUnits.Clear();
        foreach (var collider in colliders)
        {
            var unit = collider.GetComponentInParent<Unit>();
            var angle = Vector2.Angle(Anchor.up, (unit.transform.position - Anchor.position).normalized);
            if (angle > ViewAngle / 2f) continue;
            DetectedUnits.Add(unit);
        }
        if (DetectedUnits.Count > 0) Device.ActivateOutputDevices();
        if (DetectedUnits.Count == 0) Device.DeactivateOutputDevices();
    }
}
