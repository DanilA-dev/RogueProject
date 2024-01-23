using UnityEngine;

public static class DamageCalculator
{
    public static float CalculateDamage(WeaponCongif weapon, UnitStats weaponStat, float armor)
    {
        return (weapon.DefaultDamage + weaponStat.GetStatByType(weapon.ScaleStat).Value - armor - 1) / 10;
    }

    public static float CalculateCrit(WeaponCongif weapon, UnitStats weaponStat, float armor)
    {
        return weapon.DefaultDamage + weaponStat.GetStatByType(weapon.ScaleStat).Value - armor;
    }
}
