using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CustomInputContextIcon
{
    public string customInputContextString;
    public string customContextString;
    //public Sprite customInputContextIcon;
}

[CreateAssetMenu(fileName = "Device Display Settings", menuName = "Scriptable Objects/Device Display Settings", order = 1)]
public class DeviceDisplaySettings : ScriptableObject
{
    public string deviceDisplayName;

    public List<CustomInputContextIcon> customContextIcons = new List<CustomInputContextIcon>();

}