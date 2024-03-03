using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public List<GameObject> LevelPrefabs = new();

    private void Awake()
    {
        var selectedLevel = DataBase.SelectedLevel;
        var prefab = LevelPrefabs[selectedLevel];

        Instantiate(prefab, transform);
    }
}
