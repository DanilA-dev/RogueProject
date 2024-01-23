using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SimpleAnimationEventReceiver : MonoBehaviour
{
    [SerializeField] private List<AnimationEvent> _animationEvents = new List<AnimationEvent>();

    private void InvokeEventByName(string name)
    {
        var animEvent = _animationEvents.Where(a => a.EventName == name).FirstOrDefault();
        if (null != animEvent)
            animEvent.Invoke();
    }
}

[System.Serializable]
public class AnimationEvent
{
    [SerializeField] private string _eventName;
    [SerializeField] private UnityEvent _event;

    public string EventName => _eventName;

    public void Invoke() => _event?.Invoke();
}