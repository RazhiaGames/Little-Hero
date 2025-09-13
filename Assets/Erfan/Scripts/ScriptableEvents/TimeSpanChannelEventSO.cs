using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Razhia/Events/TimeSpan Event")]
public class TimeSpanChannelEventSO : ScriptableObject
{
    public UnityAction<TimeSpan> OnEventRaised;

    public void RaiseEvent(TimeSpan timeSpan)
    {
        OnEventRaised?.Invoke(timeSpan);
    }

}