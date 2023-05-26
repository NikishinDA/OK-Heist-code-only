using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreference<T>
{
    public string Name;
    public T DefaultValue;
}
public class PlayerPrefsStrings
{
    public static readonly PlayerPreference<int> Level = new PlayerPreference<int> {Name = "Level", DefaultValue = 1};
public static readonly PlayerPreference<int> TimesRotated = new PlayerPreference<int> {Name = "TimesRotated", DefaultValue = 0};
    public static readonly PlayerPreference<float> SkinProgress = new PlayerPreference<float>
        {Name = "SkinProgress", DefaultValue = 0.01f};
    public static readonly PlayerPreference<int> SkinNumber = new PlayerPreference<int> {Name = "SkinNumber", DefaultValue = -1};
    public static readonly PlayerPreference<int> SkinsUnlocked = new PlayerPreference<int> {Name = "SkinsUnlocked", DefaultValue = 0};
    
}
