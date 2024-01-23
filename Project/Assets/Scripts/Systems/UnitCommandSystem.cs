using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitCommandSystem : MonoBehaviour
{
    [SerializeField] private SelectableUnitsManager _selectableUnits;

    private void Update()
    {
        SendCommandSelectedUnits();
    }

    private void SendCommandSelectedUnits()
    {
        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            var hit = Mouse3dUtilities.GetMouseHit();
            var controllableUnits = _selectableUnits.SelectableUnits.Where(s => s.TryGetComponent(out Controllable c)
             && c.IsControllable()).Select(u => u.GetComponent<Unit>()).ToList();
            if(controllableUnits == null)
                return;

            for (int i = 0; i < controllableUnits.Count; i++)
            {
                if(hit.collider.TryGetComponent(out IDamagable damagable) && damagable != controllableUnits[i].GetComponent<IDamagable>())
                    controllableUnits[i].SetCommand(new AttackCommand(controllableUnits[i].GetComponent<CombatController>(), damagable, controllableUnits.Count));
                else
                    controllableUnits[i].SetCommand(new MoveCommand(hit.point, controllableUnits.Count, i));
            }
        }
    }
   
}
