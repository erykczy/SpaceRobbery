using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObjective : MonoBehaviour
{
    private void Start()
    {
        var gameObjective = GetComponent<GameObjective>();
        var interactableTile = GetComponent<InteractableTile>();
        var tile = GetComponent<Tile>();

        interactableTile.WhenInteracted.AddListener(gameObjective.Achieve);
        tile.WhenDestroyed.AddListener(gameObjective.Fail);
    }
}
