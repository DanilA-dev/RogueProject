using UnityEngine;
using UnityEngine.InputSystem;

public static class Mouse3dUtilities
{
    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Vector3 pos = new Vector3();
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider != null)
                pos = hit.point;
        }
        return pos;
    }

    public static RaycastHit GetMouseHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if(Physics.Raycast(ray, out RaycastHit hit))
            if(hit.collider != null)
                return hit;
        
        return new RaycastHit();
    }
}

