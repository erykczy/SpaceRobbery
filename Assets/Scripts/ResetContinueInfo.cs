using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetContinueInfo : MonoBehaviour
{
    public string ResetText;
    public string ContinueText;
    public float PlayerWinFadeInDelay;
    public float PlayerLostFadeInDelay;
    public Transform Content;
    public Image Fill;
    public TextMeshProUGUI Text;
    public Animator ContentAnimator { get; private set; }
    public GameSceneManager GameSceneManager { get; private set; }
    public GameJudge GameJudge { get; private set; }

    private void Awake()
    {
        ContentAnimator = Content.GetComponent<Animator>();
        GameSceneManager = FindObjectOfType<GameSceneManager>();
        GameJudge = FindObjectOfType<GameJudge>();
        GameJudge.WhenPlayerLost.AddListener(() => StartCoroutine(TryFadeIn(false)));
        GameJudge.WhenPlayerWin.AddListener(() => StartCoroutine(TryFadeIn(true)));
    }

    private IEnumerator TryFadeIn(bool playerWon)
    {
        yield return new WaitForSecondsRealtime(playerWon ? PlayerWinFadeInDelay : PlayerLostFadeInDelay);
        if (Content.gameObject.activeSelf) yield break;
        SetActive(true);
        ContentAnimator.SetTrigger("FadeIn");
    }

    private void Update()
    {
        Fill.fillAmount = GameSceneManager.ResetProgress / GameSceneManager.SpacebarHoldTime;
        if (GameSceneManager.ResetInProgress)
            SetActive(true);
        else if(GameJudge.CurrentGameState == GameState.Running)
            SetActive(false);
    }

    private void SetActive(bool value)
    {
        switch (GameJudge.CurrentGameState)
        {
            case GameState.Running:
                Text.text = ResetText;
                break;
            case GameState.Lost:
                Text.text = ResetText;
                break;
            case GameState.Won:
                Text.text = ContinueText;
                break;
            default:
                break;
        }
        Content.gameObject.SetActive(value);
    }
}
