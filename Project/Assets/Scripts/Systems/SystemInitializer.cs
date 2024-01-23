using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SystemInitializer : MonoBehaviour
{
    private static SystemInitializer _instance;

    [SerializeField] private List<BaseMonoSystem> _systems = new List<BaseMonoSystem>();

    private void Awake()
    {
        _instance = this;

        if(_systems.Count > 0)
            _systems.ForEach(s => s.Init());
    }

    public static T GetSystem<T>() where T : BaseMonoSystem
    {
        T returnSystem = (T)_instance._systems.Where(s => s is T).FirstOrDefault();
        return returnSystem ?? throw new ArgumentException($"Cant find system type {returnSystem} in systems");
    }


    public static SystemInitializer Instance() => _instance;
}
