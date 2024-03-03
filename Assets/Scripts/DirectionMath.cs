using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DirectionMath
{
    public static Vector2Int DirectionToIntVector(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                return Vector2Int.up;
            case Direction.Down:
                return Vector2Int.down;
            case Direction.Right:
                return Vector2Int.right;
            case Direction.Left:
                return Vector2Int.left;
        }
        throw new System.Exception("Direction vector couldn't be found!");
    }

    public static Vector2 DirectionToVector(Direction dir)
    {
        return DirectionToIntVector(dir);
    }

    public enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }
}
