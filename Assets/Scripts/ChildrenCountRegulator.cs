using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenCountRegulator : MonoBehaviour
{
    public GameObject PrefabOfChild;
    private int _count;
    public int Count { get => _count; set
        {
            _count = value;
            UpdateChildren();
        }
    }

    private void UpdateChildren()
    {
        int currentCount = transform.childCount;
        int difference = Count - currentCount;

        if (difference > 0)
        {
            for(int i = 0; i < difference; i++)
            {
                Instantiate(PrefabOfChild, transform);
            }
        }
        else if(difference < 0)
        {
            for(int i = 0; i < difference; i++)
            {
                Destroy(transform.GetChild(currentCount - i - 1).gameObject);
            }
        }
    }
}
