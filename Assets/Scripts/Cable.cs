using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public DirectionMath.Direction InputDirection;
    public DirectionMath.Direction OutputDirection;
    public Wire Wire { get; private set; }
    public Tile Tile { get; private set; }

    private void Awake()
    {
        Wire = GetComponentInParent<Wire>();
        Tile = GetComponent<Tile>();
    }

    private void OnDestroy()
    {
        Wire.OnCableDestroyed();
    }

    public Tile GetInputTile()
    {
        return Tile.Map.GetTile(Tile.Position + DirectionMath.DirectionToIntVector(InputDirection));
    }

    public Tile GetOutputTile()
    {
        return Tile.Map.GetTile(Tile.Position + DirectionMath.DirectionToIntVector(InputDirection));
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
