using UnityEngine;

public enum UnitControllState
{
    None = 0,
    Controllable,
    UnControllable
}

public class Controllable : MonoBehaviour
{
    [SerializeField] private UnitControllState _controllState;

    public UnitControllState ControllState => _controllState;

    private void Start()
    {
        _controllState = UnitControllState.Controllable;
    }

    public void ChangeControllState(UnitControllState newState)
    {
        _controllState = newState;
    }

    public bool IsControllable() => _controllState == UnitControllState.Controllable;
}
