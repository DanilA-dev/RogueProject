using System.Linq;
using UnityEngine;

public class RagdollEnabler : MonoBehaviour
{
    private Rigidbody[] _rbs;

    private void Awake()
    {
        _rbs = GetComponentsInChildren<Rigidbody>();
        
        if(_rbs != null)
            _rbs.ToList().ForEach(r => r.isKinematic = true);
    }

    public void EnableRagdoll()
    {
        if(_rbs != null)
            _rbs.ToList().ForEach(r => r.isKinematic = false);
    }
}
