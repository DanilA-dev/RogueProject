using System;
using UnityEngine;

public class MoveCommand : IUnitCommand
{
    private Vector3 _movePos;
    private int _controllableAmount;
    private int _controllableIndex;

    public MoveCommand(Vector3 movePos, int controllableAmount, int controllableIndex)
    {
        _movePos = movePos;
        _controllableAmount = controllableAmount;
        _controllableIndex = controllableIndex;
    }

    public void SetCommand(Unit unit)
    {
        
        unit?.Locomotion.StopAgent(false);
        unit?.Locomotion.ToggleRotation(true);
        unit?.Locomotion.MoveAgent(_movePos);
    }
    public void StopCommand(Unit unit) {}
}
