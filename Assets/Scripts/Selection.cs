using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection
{
    public Vector2 StartPos;
    public Vector2 EndPos;

    public List<Unit> FetchSelectedUnits()
    {
        var topRight = GetTopRight();
        var bottomLeft = GetBottomLeft();
        var size = topRight - bottomLeft;
        var colliders = Physics2D.OverlapBoxAll(bottomLeft + size / 2, size, 0);

        var units = new List<Unit>();
        foreach (var collider in colliders)
        {
            if (collider.attachedRigidbody != null && collider.attachedRigidbody.TryGetComponent<Unit>(out var unit))
            {
                if (!units.Contains(unit))
                    units.Add(unit);
            }
        }
        return units;
    }

    public Vector2 GetBottomLeft()
    {
        return new Vector2(Mathf.Min(StartPos.x, EndPos.x), Mathf.Min(StartPos.y, EndPos.y));
    }

    public Vector2 GetTopRight()
    {
        return new Vector2(Mathf.Max(StartPos.x, EndPos.x), Mathf.Max(StartPos.y, EndPos.y));
    }
}
