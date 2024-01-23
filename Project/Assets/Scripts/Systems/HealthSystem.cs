using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IDamagable
{
    private enum MaxHealthSetup
    {
        Manually,
        Vitality_Stat
    }

    [SerializeField, ReadOnly] private float _currentHealth;
    [SerializeField] private MaxHealthSetup _maxHealthSetup;

    [ShowIf("_maxHealthSetup", MaxHealthSetup.Manually)]
    [SerializeField] private int _maxHealth;
    [ShowIf("_maxHealthSetup", MaxHealthSetup.Vitality_Stat)]
    [SerializeField] private UnitStats _stats;
    [SerializeField] private bool _canBeDamaged;

    public event Action<float, float> OnHealthChange;
    public UnityEvent OnDie;
    public UnityEvent OnDamaged;

    public float CurrentHealth {get => _currentHealth; private set => _currentHealth = Mathf.Clamp(value, 0, MaxHealth);}
    public int MaxHealth {get; private set;}

    public bool CanBeDamaged => _canBeDamaged;
    public GameObject GameObject => this.gameObject;

    public void Init()
    {
        MaxHealth = _maxHealthSetup == MaxHealthSetup.Manually ? _maxHealth : _stats.GetStatByType(StatType.Vitality).Value;
        CurrentHealth = MaxHealth;
    }

    public void ChangeHealth(float value)
    {
        CurrentHealth += value;
        OnHealthChange?.Invoke(CurrentHealth, MaxHealth);
    }

    public void Damage(float damageAmount, object damageDelear)
    {
        Debug.Log($"damaged with {damageAmount} by {damageDelear}");

        CurrentHealth -= damageAmount;
        OnHealthChange?.Invoke(CurrentHealth, MaxHealth);
        OnDamaged?.Invoke();
        if(CurrentHealth <= 0)
            Die();
    }

    private void Die()
    {
        _canBeDamaged = false;
        OnDie?.Invoke();
    }
}
