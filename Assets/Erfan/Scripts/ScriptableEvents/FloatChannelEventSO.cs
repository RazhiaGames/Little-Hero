    using System;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Float Event", menuName = "Razhia/Events/Float Event")]
    public class FloatChannelEventSO : ScriptableObject
    {
        public event Action<float> OnEventRaised;

        public void RaiseEvent(float value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
