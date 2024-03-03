using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int LevelIndex { get; set; }
    public int GameSceneIndex { get; set; }

    private void Start()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = (LevelIndex + 1).ToString();

        var eventTrigger = GetComponent<EventTrigger>();
        var entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((e) => OnClicked());
        eventTrigger.triggers.Add(entry);
    }

    private void OnClicked()
    {
        DataBase.SelectedLevel = LevelIndex;
        SceneManager.LoadScene(GameSceneIndex);
    }
}
