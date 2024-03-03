using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    private Dictionary<(int, int), Tile> Tiles = new();
    private static readonly List<Vector2Int> Directions = new()
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.right,
        Vector2Int.left
    };

    private void Awake()
    {
        foreach (var tile in GetComponentsInChildren<Tile>())
        {
            var pos = tile.Position;
            Tiles.Add((pos.x, pos.y), tile);
        }
    }

    public Tile GetTile(Vector2Int pos)
    {
        return Tiles.GetValueOrDefault((pos.x, pos.y), null);
    }

    public void OnTileDestroyed(Vector2Int pos)
    {
        Tiles.Remove((pos.x, pos.y));
    }

    public List<Tile> GetSideAdjacentTiles(Vector2Int pos)
    {
        var list = new List<Tile>();
        foreach (var dir in Directions)
        {
            var tile = GetTile(pos + dir);
            if (tile != null) list.Add(tile);
        }
        return list;
    }
}
