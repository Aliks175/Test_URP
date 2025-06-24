using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CraftSettings", menuName = "System/Craft/CraftSettings")]
public class CraftSettings : ScriptableObject
{
    public List<CraftCombination> craftCombinations;

    public void Initialization()
    {
        if (craftCombinations == null) return;
        foreach (var item in craftCombinations)
        {
            item.Source.Sort();
        }
    }
}

[Serializable]
public class CraftCombination
{
    public List<string> Source;
    public GameObject Result;
}