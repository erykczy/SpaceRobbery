using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameJudge : MonoBehaviour
{
    public GameState CurrentGameState = GameState.Running;
    public UnityEvent WhenPlayerWin;
    public UnityEvent WhenPlayerLost;
    private List<GameObjective> Objectives = new();

    private void Start()
    {
        Objectives = FindObjectsByType<GameObjective>(FindObjectsSortMode.None).ToList();
        foreach (var objective in Objectives)
        {
            objective.WhenAchieved.AddListener(() => OnObjectiveAchieved(objective));
            objective.WhenFailed.AddListener(() => OnObjectiveFailed(objective));
        }
    }

    private void OnObjectiveAchieved(GameObjective objective)
    {
        if (CurrentGameState != GameState.Running) return;
        Objectives.Remove(objective);
        if (Objectives.Count == 0) StopGame(true);
    }

    private void OnObjectiveFailed(GameObjective objective)
    {
        if (CurrentGameState != GameState.Running) return;
        StopGame(false);
    }

    private void StopGame(bool playerWon)
    {
        if (playerWon)
        {
            CurrentGameState = GameState.Won;
            DataBase.CurrentLevel++;
            WhenPlayerWin.Invoke();
        }
        else
        {
            CurrentGameState = GameState.Lost;
            WhenPlayerLost.Invoke();
        }
    }
}
