using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelectionSystem : MonoBehaviour
{
    [SerializeField] private RectTransform _selectArea;
    [SerializeField] private SelectableUnitsManager _selectableUnitsManager;
    [SerializeField] private SelectionBox _selectBox;

    private Vector2 _lastMouseScreenPos;
    private Vector3 _lastMouseWorldPos;

    private void Start()
    {
        _selectBox.Init(_selectableUnitsManager);
    }

    private void Update()
    {
        UpdateLastMousePosition();
        TrySelectSingleUnit();
        CreateSelectArea();
    }

    private void TrySelectSingleUnit()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit mouseHit = Mouse3dUtilities.GetMouseHit();
            if(mouseHit.collider.TryGetComponent(out SelectableUnit selectable))
            {
                if(_selectableUnitsManager.SelectableUnits.Count > 0)
                {
                    var lastSelected = _selectableUnitsManager.SelectableUnits[_selectableUnitsManager.SelectableUnits.Count - 1];
                    _selectableUnitsManager.RemoveSelected(lastSelected);
                }
                _selectableUnitsManager.AddSelected(selectable);
            }
            else
                 _selectableUnitsManager.RemoveAll();
        }
    }

    private void UpdateLastMousePosition()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            _selectArea.sizeDelta = Vector2.zero;
            _selectArea.gameObject.SetActive(true);
            _lastMouseScreenPos = Mouse.current.position.ReadValue();

            _selectBox.gameObject.SetActive(true);
            _lastMouseWorldPos = Mouse3dUtilities.GetMouseWorldPosition();
            _lastMouseWorldPos.y = 0;
            _selectBox.transform.localPosition = _lastMouseWorldPos;
        }
    }

    private void CreateSelectArea()
    {
        if(Mouse.current.leftButton.IsPressed())
        {
            ResizeSelectionArea();
        }
        else if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _selectArea.gameObject.SetActive(false);
            _selectBox.gameObject.SetActive(false);
        }
    }

    private void ResizeSelectionArea()
    {
        DrawVisualArae();
        CreateSelectionBox();
    }

    private void CreateSelectionBox()
    {
        float widht = Mouse3dUtilities.GetMouseWorldPosition().x - _lastMouseWorldPos.x;
        float height = Mouse3dUtilities.GetMouseWorldPosition().z - _lastMouseWorldPos.z;
        var size = new Vector3(Mathf.Abs(widht), 5f, Mathf.Abs(height));
        _selectBox.transform.localScale = size;
        _selectBox.transform.localPosition = _lastMouseWorldPos + new Vector3(widht / 2, 0, height / 2);
    }

    private void DrawVisualArae()
    {
        float x = Mouse.current.position.ReadValue().x - _lastMouseScreenPos.x;
        float y = Mouse.current.position.ReadValue().y - _lastMouseScreenPos.y;
        var size = new Vector2(Mathf.Abs(x), Mathf.Abs(y));
        _selectArea.anchoredPosition = _lastMouseScreenPos + new Vector2(x / 2, y / 2);
        _selectArea.sizeDelta = size;
    }
}
