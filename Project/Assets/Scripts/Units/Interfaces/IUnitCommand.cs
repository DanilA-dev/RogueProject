using UnityEngine;

public interface IUnitCommand
{
    public void SetCommand(Unit unit);
    public void StopCommand(Unit unit);
}
