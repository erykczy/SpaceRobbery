using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjective : MonoBehaviour
{
    public UnityEvent WhenAchieved = new();
    public UnityEvent WhenFailed = new();

    public void Achieve()
    {
        WhenAchieved.Invoke();
        Destroy(this);
    }

    public void Fail()
    {
        WhenFailed.Invoke();
        Destroy(this);
    }
}
