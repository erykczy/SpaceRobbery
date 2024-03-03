using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonText : MonoBehaviour
{
    public Color NormalColor;
    public Color HoveredColor;
    public EventTrigger EventTrigger { get; private set; }
    public TextMeshProUGUI Text { get; private set; }

    private void Awake()
    {
        EventTrigger = GetComponentInParent<EventTrigger>();
        Text = GetComponent<TextMeshProUGUI>();

        var pointerEnter = new EventTrigger.Entry();
        pointerEnter.eventID = EventTriggerType.PointerEnter;
        pointerEnter.callback.AddListener((e) => OnPointerEnter());
        EventTrigger.triggers.Add(pointerEnter);

        var pointerExit = new EventTrigger.Entry();
        pointerExit.eventID = EventTriggerType.PointerExit;
        pointerExit.callback.AddListener((e) => OnPointerExit());
        EventTrigger.triggers.Add(pointerExit);
    }

    private void OnEnable()
    {
        Text.color = NormalColor;
    }

    private void OnPointerEnter()
    {
        Text.color = HoveredColor;
    }

    private void OnPointerExit()
    {
        Text.color = NormalColor;
    }
}
