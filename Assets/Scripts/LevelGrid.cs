using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public int DebugCurrentLevel = 0; // TODO to remove
    public int LevelsCount;
    public int GameSceneIndex;
    public GameObject ActiveLevelButtonPrefab;
    public GameObject DisabledLevelButtonPrefab;

    private void Start()
    {
        DataBase.CurrentLevel = DebugCurrentLevel;
        var currentLevel = DataBase.CurrentLevel;
        for(int i = 0; i < LevelsCount; i++)
        {
            if(i <= currentLevel)
            {
                var obj = Instantiate(ActiveLevelButtonPrefab, transform);
                var button = obj.GetComponent<LevelButton>();
                button.LevelIndex = i;
                button.GameSceneIndex = GameSceneIndex;
            }
            else
            {
                Instantiate(DisabledLevelButtonPrefab, transform);
            }
        }
    }
}
