using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Razhia/Events/Void Event")]
public class VoidChannelEventSO : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }

}
