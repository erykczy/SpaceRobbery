using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DataBase
{
    public static int CurrentLevel { get => GetOrInitialize("CurrentLevel", 0); set => SetAndSave("CurrentLevel", value); }
    public static int SelectedLevel = 0;

    private static int GetOrInitialize(string key, int defaultValue)
    {
        if(!PlayerPrefs.HasKey(key))
        {
            SetAndSave(key, defaultValue);
        }
        return PlayerPrefs.GetInt(key);
    }

    private static void SetAndSave(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }
}
