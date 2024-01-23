using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class WeaponSettings
{
   [SerializeField] private WeaponCongif _startWeapon;
   [SerializeField] private WeaponCongif _currentWeapon;
   [SerializeField] private List<WeaponHandItem> _rightHandWeapons;


   private WeaponCongif _previousWeapon;
   private float _currentWeaponAttackTime;
   private float _currentWeaponCooldown;

   public WeaponCongif CurrentWeapon => _currentWeapon;
   public float CurrentWeaponAttackTime { get => _currentWeaponAttackTime; set => _currentWeaponAttackTime = value; }
   public float CurrentWeaponCooldown { get => _currentWeaponCooldown; set => _currentWeaponCooldown = value; }

   public void Init(Unit unit)
   {
      UpdateCurrentWeapon(_startWeapon, unit);
   }

   public void UpdateCurrentWeapon(WeaponCongif newWeapon, Unit unit)
   {
      _previousWeapon = _currentWeapon;
      _currentWeapon = newWeapon;
      _currentWeaponAttackTime = _currentWeapon.TimeToAttack;
      _currentWeaponCooldown = _currentWeapon.AttackCooldown;
      EquipVisual(_currentWeapon);
    
      unit.View.SetBool(_previousWeapon?.AnimationLocomotionBool, false);
      unit.View.SetBool(_currentWeapon?.AnimationLocomotionBool, true);
   }
    private void EquipVisual(WeaponCongif currentWeapon)
    {
        if(_rightHandWeapons != null)
            _rightHandWeapons.ForEach(w => w.gameObject.SetActive(false));
        if(currentWeapon.Category == WeaponCongif.WeaponCategory.Hands && currentWeapon.Id == 0)
            return;
        var rightView = _rightHandWeapons.Where(w => w.HandId == currentWeapon.Id).FirstOrDefault();
        if(rightView != null)
            rightView.gameObject.SetActive(true);
        else
            Debug.Log($"Can't find weapon with id {currentWeapon.Id}");
    }
}