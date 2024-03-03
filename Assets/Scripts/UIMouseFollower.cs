using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMouseFollower : MonoBehaviour
{
    public RectTransform RectTransform { get; private set; }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        RectTransform.position = Input.mousePosition;
    }
}
