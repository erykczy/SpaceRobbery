using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrainedLineRenderer : MonoBehaviour
{
    public List<Vector3> RequestedPoints = new();
    public float Min;
    public float Max;
    public LineRenderer LineRenderer { get; private set; }

    private void Awake()
    {
        LineRenderer = GetComponent<LineRenderer>();
    }

    public void UpdateRendering()
    {
        if (RequestedPoints.Count < 2) return;
        if (LineRenderer == null) LineRenderer = GetComponent<LineRenderer>();
        var newPoints = new List<Vector3>();

        float totalDistance = 0f;
        Vector3 prevPoint = RequestedPoints[0];
        for(int i = 1; i < RequestedPoints.Count; i++)
        {
            Vector3 currentPoint = RequestedPoints[i];
            var distanceVec = currentPoint - prevPoint;
            var distance = distanceVec.magnitude;

            if (Min >= totalDistance && Min <= totalDistance + distance)
            {
                var d = Min - totalDistance;
                var delta = distanceVec.normalized * d;
                newPoints.Add(prevPoint + delta);
                continue;
            }
            if(totalDistance + distance > Min && totalDistance + distance < Max)
            {
                newPoints.Add(currentPoint);
                continue;
            }
            if(totalDistance + distance >= Max)
            {
                var d = Max - totalDistance;
                var delta = distanceVec.normalized * d;
                newPoints.Add(prevPoint + delta);
                continue;
            }
        }
        LineRenderer.positionCount = newPoints.Count;
        LineRenderer.SetPositions(newPoints.ToArray());
    }
}
