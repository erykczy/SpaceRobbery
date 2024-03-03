using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public static UserInputConfig Config { get; private set; }

    private void Awake()
    {
        Config = new();
    }

    private void OnEnable()
    {
        Config.Enable();
    }

    private void OnDisable()
    {
        Config.Disable();
    }
}
