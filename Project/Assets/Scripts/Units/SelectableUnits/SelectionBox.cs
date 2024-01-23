using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SelectionBox : MonoBehaviour
{
    private SelectableUnitsManager _selectManager;

    public BoxCollider Coll {get; private set;}

    public void Init(SelectableUnitsManager selectManager)
    {
        _selectManager = selectManager;
        Coll = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out SelectableUnit selectable) && !selectable.IsSelected)
            _selectManager.AddSelected(selectable);
    }

    private void OnTriggerExit(Collider other)
    {
        _selectManager.RemoveAll();
    }
    
}
