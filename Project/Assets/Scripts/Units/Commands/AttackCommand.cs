using UnityEngine;

public class AttackCommand : IUnitCommand
{
    private CombatController _combatController;
    private IDamagable _unitToAttack;
    private int _controllableAmount;

    public AttackCommand(CombatController combatController, IDamagable unitToAttack, int controllableAmount)
    {
        _combatController = combatController;
        _unitToAttack = unitToAttack;
        _controllableAmount = controllableAmount;
    }

    public void SetCommand(Unit unit)
    {
        if(_unitToAttack.GameObject.TryGetComponent(out Unit u))
        {
            if(u.Faction == unit.Faction)
            {
                StopCommand(unit);
                return;
            }
        }

       _combatController.AttackCommand(_unitToAttack, _controllableAmount);
    }

    public void StopCommand(Unit unit) 
    {
        unit?.StopAllCoroutines();
        _combatController?.ExitCombatState();
    }

}
