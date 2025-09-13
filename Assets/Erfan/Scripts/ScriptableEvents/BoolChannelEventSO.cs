using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Razhia/Events/Bool Event")]
public class BoolChannelEventSO : ScriptableObject
{
    public UnityAction<bool> OnEventRaised;

    public void RaiseEvent(bool value)
    {
        OnEventRaised?.Invoke(value);
    }

}