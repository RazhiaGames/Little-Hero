using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Razhia/Events/String Event")]
public class StringChannelEventSO : ScriptableObject
{
    public UnityAction<string> OnEventRaised;

    public void RaiseEvent(string value)
    {
        OnEventRaised?.Invoke(value);
    }

}