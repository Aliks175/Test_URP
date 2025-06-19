using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemLevel : MonoBehaviour
{
    [SerializeField] private LevelLine levelLine;
    [SerializeField] private List<LevelUps> listUp;
    public int Level { get { return _level; } private set { _level = value; } }

    private int _еxperience;
    private int _level;
    private float _expDoNextLevel;
    private float _procentExpDoNextLevel;

    public event Action<SystemLevelData> OnSetUp;
    public event Action<SystemLevelData> OnChangeExp;
    public event Action OnLevelUp;


    private void Start()
    {
        SetUp();
    }

    /// <summary>
    /// Проверка достигнут ли следующий уровень
    /// </summary>
    private void CheckUpdateLevel()
    {
        int Leveltemp = Mathf.FloorToInt(levelLine.CurveLevel.Evaluate(_еxperience));
        if (Leveltemp > Level)
        {
            for (; Level < Leveltemp; Level++)
            {
                foreach (var level in listUp)
                {
                    level.LevelUp(this);
                }
                OnLevelUp?.Invoke();
            }
        }
    }

    /// <summary>
    /// Добавить еxperience
    /// </summary>
    /// <param name="newExp"></param>
    public void SetEx(int newExp)
    {
        newExp = Mathf.Abs(newExp);
        _еxperience += newExp;

        CheckUpdateLevel();
        CalculateExp();

        OnChangeExp?.Invoke(new SystemLevelData()
        {
            _nowExp = _еxperience,
            _level = Level,
            _expDoNextLevel = _expDoNextLevel,
            _rangeExpDoNextLevel = _procentExpDoNextLevel
        });
    }

    /// <summary>
    /// Просчет и заполенние полей , сколько осталось опыта , и процентное соотношение
    /// </summary>
    [Button]

    private void CalculateExp()
    {
        var keysLevelUpLine = levelLine.CurveLevel.keys;// получаем все точки кривой 1 точка ровна 1 уровню
        float ExpNextLevel = 0; // количество опыта следующего уровня 

        if (keysLevelUpLine.Length < Level + 1)
        {
            _expDoNextLevel = 0;
            _procentExpDoNextLevel = 1f;
            return;
        } // если следующий уровень больше чем есть в кривой , то мы ничего не делаем игрок достиг максимального уровня 

        ExpNextLevel = keysLevelUpLine[Level].time;// количество опыта следующего уровня 
        _expDoNextLevel = ExpNextLevel - _еxperience; // количество опыта до следующего уровня 

        float doNowLevelToNextLevel = (_еxperience - keysLevelUpLine[Level - 1].time);// количество опыта полученого после получения текущего уровня 
        float rangeExpDoNowLevelToNextLevel = (ExpNextLevel - keysLevelUpLine[Level - 1].time);// Количество опыта от текущего уровня до следующего 

        float Procent = doNowLevelToNextLevel / rangeExpDoNowLevelToNextLevel;// процент полученного опыта до получения следующего уровня , где 100% или 1.0 это получение след уровня

        _procentExpDoNextLevel = Mathf.Clamp01(Procent);
    }

    /// <summary>
    /// Стартовая предустановка 
    /// </summary>
    public void SetUp()
    {
        Level = Mathf.FloorToInt(levelLine.CurveLevel.Evaluate(_еxperience));
        CalculateExp();
        OnSetUp?.Invoke(new SystemLevelData()
        {
            _nowExp = _еxperience,
            _level = Level,
            _expDoNextLevel = _expDoNextLevel,
            _rangeExpDoNextLevel = _procentExpDoNextLevel
        });
    }
}
/// <summary>
/// Структура содержещая информацию об уровне
/// </summary>
public struct SystemLevelData
{
    public int _nowExp;
    public float _expDoNextLevel;
    public float _rangeExpDoNextLevel;
    public int _level;
}
