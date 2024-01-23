using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitStats _stats;
    [SerializeField] private UnitFaction _faction;
    [SerializeField] private UnitView _view;
    [SerializeField] private CombatController _combatController;
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private UnitLocomotion _locomotion;

    private IUnitCommand _currentCommand;

    public event Action OnNewCommand;

    public UnitLocomotion Locomotion => _locomotion;
    public UnitStats Stats => _stats;
    public UnitView View => _view;
    public UnitFaction Faction => _faction;

    private void Awake()
    {
        InitComponents();
    }

    private void InitComponents()
    {
        _combatController.Init(this);
        _healthSystem.Init();
    }

    private void Update()
    {
        _view.UpdateLocomotion(_locomotion.NavAgent.velocity.magnitude /_locomotion.NavAgent.speed);
    }

    public void SetCommand(IUnitCommand command)
    {
        if(_currentCommand != command)
            _currentCommand?.StopCommand(this);

        _currentCommand = command;
        _currentCommand.SetCommand(this);
        OnNewCommand?.Invoke();
        Debug.Log(command);
    }
}
