using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerStat : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;

    [SerializeField] private Slider sliderRangeExpDoNextLevel;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _expNow;
    [SerializeField] private TextMeshProUGUI _expNextLevel;

    private void OnEnable()
    {
        characterData.SystemLevel.OnChangeExp += ChangeExp;
        characterData.SystemLevel.OnLevelUp += ChangeLevel;
        characterData.SystemLevel.OnSetUp += SetUp;
    }

    private void OnDisable()
    {
        characterData.SystemLevel.OnChangeExp -= ChangeExp;
        characterData.SystemLevel.OnLevelUp -= ChangeLevel;
        characterData.SystemLevel.OnSetUp -= SetUp;
    }

    private void ChangeExp(SystemLevelData expData)// ������� �� ��������� ����� ����� 
    {
        UpdateUi(expData);// ��������� Ui
    }

    private void ChangeLevel()// ���������� ������� ��������� ������ 
    {
        Debug.Log("Level UP");
    }

    private void UpdateUi(SystemLevelData expData)// ��������� Ui ��������� ���� 
    {
        _expNow.SetText($"EXP : {expData._nowExp.ToString()}");

        _level.SetText($"Level : {expData._level.ToString()}");

        _expNextLevel.SetText($"Lost EXP : {expData._expDoNextLevel.ToString()}");

        sliderRangeExpDoNextLevel.value = expData._rangeExpDoNextLevel;
    }

    private void SetUp(SystemLevelData expData) // ������������� �������� ���� 
    {
        UpdateUi(expData);
    }

}
