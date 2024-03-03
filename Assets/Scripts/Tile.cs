using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public UnityEvent WhenDestroyed { get; private set; } = new();
    public TileMap Map { get; private set; }
    public Vector2Int Position { get => new(Mathf.RoundToInt(transform.localPosition.x), Mathf.RoundToInt(transform.localPosition.y)); }

    private void Awake()
    {
        Map = GetComponentInParent<TileMap>();
    }

    private void OnDestroy()
    {
        WhenDestroyed.Invoke();
        Map.OnTileDestroyed(Position);
    }
}
