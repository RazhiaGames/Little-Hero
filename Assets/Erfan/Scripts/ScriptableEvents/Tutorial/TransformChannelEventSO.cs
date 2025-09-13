using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Razhia/Events/Transform EventChannel")]
public class TransformChannelEventSO : ScriptableObject
{
    public UnityAction<Transform> OnEventRaised;
    public void RaiseEvent(Transform value)
    {
        OnEventRaised?.Invoke(value);
    }
}
