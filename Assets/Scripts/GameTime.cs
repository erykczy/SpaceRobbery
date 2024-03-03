using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public float TimeScale = 0.1f;
    public Animator Animator { get; private set; }
    public GameJudge GameJudge { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        GameJudge = FindObjectOfType<GameJudge>();
        GameJudge.WhenPlayerLost.AddListener(OnPlayerLost);
    }

    private void OnPlayerLost()
    {
        Animator.SetTrigger("PlayerLost");
    }

    private void Update()
    {
        Time.timeScale = TimeScale;
    }
}
