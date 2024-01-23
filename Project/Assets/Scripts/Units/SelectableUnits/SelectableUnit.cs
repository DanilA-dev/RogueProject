using UnityEngine;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField] private UnitInterface _interface;
    [SerializeField] private bool _isSelected;

    public bool IsSelected => _isSelected;

    public void Select()
    {
        _isSelected = true;
        _interface.ToggleSelectImg(_isSelected);
    }

    public void UnSelect()
    {
        _isSelected = false;
        _interface.ToggleSelectImg(_isSelected);
    }
}
