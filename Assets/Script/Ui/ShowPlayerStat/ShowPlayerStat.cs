using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerStat : MonoBehaviour
{
    [SerializeField] private SystemLevel characterData;

    [SerializeField] private Slider sliderRangeExpDoNextLevel;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _expNow;
    [SerializeField] private TextMeshProUGUI _expNextLevel;

    private void Awake()
    {
        characterData.OnChangeExp += ChangeExp;
        characterData.OnLevelUp += ChangeLevel;
        characterData.OnSetUp += SetUp;
    }

    private void OnDisable()
    {
        characterData.OnChangeExp -= ChangeExp;
        characterData.OnLevelUp -= ChangeLevel;
        characterData.OnSetUp -= SetUp;
    }

    private void ChangeExp(SystemLevelData expData)// Событие по изменению колво опыта 
    {
        UpdateUi(expData);// обновляем Ui
    }

    private void ChangeLevel()// отработало сабытие получение уровня 
    {
        Debug.Log("Level UP");
    }

    private void UpdateUi(SystemLevelData expData)// обновляем Ui заполняем поля 
    {
        _expNow.SetText($"EXP : {expData._nowExp.ToString()}");

        _level.SetText($"Level : {expData._level.ToString()}");

        _expNextLevel.SetText($"Lost EXP : {expData._expDoNextLevel.ToString()}");

        sliderRangeExpDoNextLevel.value = expData._rangeExpDoNextLevel;
    }

    private void SetUp(SystemLevelData expData) // предустановка обновить поля 
    {
        UpdateUi(expData);
    }

}
