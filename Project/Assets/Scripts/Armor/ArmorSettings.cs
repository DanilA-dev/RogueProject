using UnityEngine;
using Sirenix.OdinInspector;
using System;

[Serializable]
public class ArmorSettings
{
    public enum ArmorDefenceType
    {
        Simplifeid,
        Equipable
    }

    [SerializeField] private ArmorDefenceType _type;

    [ShowIf("_type",  ArmorDefenceType.Simplifeid)]
    [SerializeField] private SimplifiedArmorType _simpleDefence;
    [ShowIf("_type",  ArmorDefenceType.Equipable)]
    [SerializeField] private EquipableArmorType _equipableDefence;

    public EquipableArmorType EquipableArmor => _equipableDefence;

    public float Defence {get; private set;}

    public void Init()
    {
        Defence = _type == ArmorDefenceType.Simplifeid ? _simpleDefence.Defence.Value : _equipableDefence.Defence.Value;
    }
}
