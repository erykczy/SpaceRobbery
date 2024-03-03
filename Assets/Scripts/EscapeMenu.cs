using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    public bool Enabled = false;
    public Transform Content;
    public GameSceneManager GameSceneManager { get; private set; }

    private void Awake()
    {
        GameSceneManager = FindObjectOfType<GameSceneManager>();
    }

    private void Start()
    {
        UserInput.Config.GameScene.EscapeMenu.performed += (e) => OnKeyPress();
    }

    private void OnKeyPress()
    {
        if (Enabled)
            Close();
        else
            Open();
    }

    public void Open()
    {
        Enabled = true;
        Content.gameObject.SetActive(true);
    }

    public void Close()
    {
        Enabled = false;
        Content.gameObject.SetActive(false);
    }

    public void ResetGame()
    {
        GameSceneManager.ResetScene();
    }

    public void OpenLevelSelector()
    {
        GameSceneManager.GoToLevelSelector();
    }

    public void ExitGame()
    {
        GameSceneManager.ExitGame();
    }
}
