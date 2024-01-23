using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using System;

public enum StatType
{
    None = 0,
    Vitality,
    Strength,
    Dexterity,
    Luck
}

[CreateAssetMenu(menuName ="Data/Unit Stats")]
public class UnitStats : ScriptableObject
{
    [SerializeField] private List<UnitStat> _stats = new List<UnitStat>();

    public UnitStat GetStatByType(StatType type)
    {
        return _stats.Where(s => s.Stat == type).FirstOrDefault() ??
        throw new ArgumentException($"Trying to get stat {type} but it doesn't exist in list");
    }
    
    [Button]
    private void SetStatsToDefault()
    {
        if(_stats.Count <= 0)
            return;

        _stats.ForEach(s => s.SetStat(10));
    }


    [System.Serializable]
    public class UnitStat
    {
        [SerializeField] public StatType _stat;
        [SerializeField] private int _value;

        public StatType Stat => _stat;
        public int Value {get => _value; set => _value = Mathf.Clamp(value, 0, 100);}

        public void ChangeStat(int newValue) => _value += newValue;
        public void SetStat(int value) => _value = value;
    }
}


