using System.Collections.Generic;
using UnityEngine;

public class SelectableUnitsManager : MonoBehaviour
{
    [SerializeField] private List<SelectableUnit> _selectableUnits = new List<SelectableUnit>();

    public IReadOnlyList<SelectableUnit> SelectableUnits => _selectableUnits;

   
    public void AddSelected(SelectableUnit unit)
    {
        unit.Select();
        _selectableUnits.Add(unit);
    }

    public void RemoveSelected(SelectableUnit unit)
    {
        if(_selectableUnits.Contains(unit))
        {
            unit.UnSelect();
            _selectableUnits.Remove(unit);
        }
    }

    public void RemoveAll()
    {
        if(_selectableUnits.Count > 0)
            _selectableUnits.ForEach(s => s.UnSelect());
        
        _selectableUnits.Clear();
    }



}
