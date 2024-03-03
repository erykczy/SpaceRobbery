using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRenderer : MonoBehaviour
{
    public int PointsCount = 10;
    public float Radius;
    public LineRenderer LineRenderer { get; private set; }

    private void Awake()
    {
        LineRenderer = GetComponent<LineRenderer>();
    }

    private void OnValidate()
    {
        LineRenderer = GetComponent<LineRenderer>();
        CreateCircle();
    }

    private void CreateCircle()
    {
        LineRenderer.positionCount = PointsCount + 1;

        var radiusOffset = LineRenderer.startWidth / 2f;
        for (int i = 0; i < PointsCount; i++)
        {
            var angle = 2 * Mathf.PI / PointsCount * i;
            var y = (Radius + radiusOffset) * Mathf.Cos(angle);
            var x = (Radius + radiusOffset) * Mathf.Sin(angle);
            LineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
        LineRenderer.SetPosition(PointsCount, LineRenderer.GetPosition(0));
    }
}
