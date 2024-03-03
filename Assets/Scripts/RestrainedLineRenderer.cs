using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrainedLineRenderer : MonoBehaviour
{
    public List<Vector3> RequestedPoints = new();
    private float _filament = 0f;
    public float Filament { get => _filament; set => SetFill(value); }
    public LineRenderer LineRenderer { get; private set; }

    private void Awake()
    {
        LineRenderer = GetComponent<LineRenderer>();
    }

    private void SetFill(float newValue)
    {
        _filament = newValue;
        UpdateRendering();
    }

    private void UpdateRendering()
    {
        if (RequestedPoints.Count < 2) return;
        if (LineRenderer == null) LineRenderer = GetComponent<LineRenderer>();
        var newPoints = new List<Vector3>() { RequestedPoints[0] };

        var filamentLeft = Filament;
        Vector3 prevPoint = RequestedPoints[0];
        for(int i = 1; i < RequestedPoints.Count; i++)
        {
            Vector3 currentPoint = RequestedPoints[i];
            var distanceVec = currentPoint - prevPoint;
            var distance = distanceVec.magnitude;
            if(filamentLeft >= distance)
            {
                filamentLeft -= distance;
                newPoints.Add(currentPoint);
                prevPoint = currentPoint;
                continue;
            }
            else
            {
                var t = filamentLeft / distance;
                var delta = distanceVec * t;
                newPoints.Add(prevPoint + delta);
                break;
            }
        }
        LineRenderer.positionCount = newPoints.Count;
        LineRenderer.SetPositions(newPoints.ToArray());
    }
}
