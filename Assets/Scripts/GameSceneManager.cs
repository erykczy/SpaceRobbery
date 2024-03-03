using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public int LevelSelectorSceneIndex;
    public float SpacebarHoldTime;
    public GameJudge GameJudge { get; private set; }
    public bool ResetInProgress { get; private set; } = false;
    public float ResetProgress { get; private set; } = 0f;

    private void Awake()
    {
        GameJudge = FindObjectOfType<GameJudge>();
    }

    private void Start()
    {
        UserInput.Config.GameScene.ContinueOrReset.started += (e) => OnSpacebarStart();
        UserInput.Config.GameScene.ContinueOrReset.canceled += (e) => OnSpacebarStop();
    }

    private void OnSpacebarStart()
    {
        ResetInProgress = true;
    }

    private void OnSpacebarStop()
    {
        ResetInProgress = false;
        ResetProgress = 0f;
    }

    private void Update()
    {
        if(ResetInProgress)
        {
            ResetProgress += Time.unscaledDeltaTime;

            if(ResetProgress >= SpacebarHoldTime)
            {
                if (GameJudge.CurrentGameState == GameState.Won)
                    GoToNextLevel();
                else
                    ResetScene();
            }
        }
    }

    public void GoToNextLevel()
    {
        DataBase.SelectedLevel++;
        ResetScene();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToLevelSelector()
    {
        SceneManager.LoadScene(LevelSelectorSceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
