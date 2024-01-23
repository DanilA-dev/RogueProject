using UnityEngine;
using Sirenix.OdinInspector;
using System;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Data/WeaponConfig")]
public class WeaponCongif : ScriptableObject
{
    public enum WeaponCategory
    {
        None = 0,
        Hands,
        OneHanded,
        TwoHanded,
        Bow,
        Staff
    }

    [SerializeField] private WeaponCategory _category;
    [SerializeField] private int _handWeaponId;
    [SerializeField] private StatType _scalableStat;
    [SerializeField] private float _defaultDamage;
    [SerializeField] private float _critChance;
    [SerializeField, Range(0.1f, 50)] private float _range;
    [SerializeField, Range(0, 10)] private float _timeToAttack;
    [SerializeField, Range(0, 10)] private float _attackCooldown;
    [SerializeField] private string _animationLocomotionBool;
    [SerializeField] private string[] _animationAttackTriggers;
    [PreviewField(75, ObjectFieldAlignment.Right)]
    [SerializeField] private Sprite _icon;

    public StatType ScaleStat => _scalableStat;
    public WeaponCategory Category => _category;
    public float DefaultDamage => _defaultDamage;
    public float CritChance => _critChance;
    public int Id => _handWeaponId;
    public float Range  => _range; 
    public float TimeToAttack => _timeToAttack;
    public float AttackCooldown => _attackCooldown;
    public string AnimationLocomotionBool => _animationLocomotionBool;
    public string[] AnimationAttackTriggers => _animationAttackTriggers;
    public Sprite Icon => _icon;

    public bool IsCrit(Action OnCrit)
    {
        float rand = Random.Range(0, 101f);
        if(rand < _critChance)
        {
            OnCrit?.Invoke();
            return true;
        }
        return false;
    }
}
