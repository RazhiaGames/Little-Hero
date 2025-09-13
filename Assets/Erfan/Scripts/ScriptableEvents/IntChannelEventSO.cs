using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Int Event", menuName = "Razhia/Events/Int Event")]
public class IntChannelEventSO : ScriptableObject
{
    public event Action<int> OnEventRaised;

    public void RaiseEvent(int value)
    {
        OnEventRaised?.Invoke(value);
    }
}
