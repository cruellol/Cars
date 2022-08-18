using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars
{
    public abstract class BaseInputController : MonoBehaviour
    {
        public float Acceleration { get; protected set; }
        public float Rotate { get; protected set; }

        public event System.Action<bool> OnHandBreak;
        protected void CallHandBrake(bool value) => OnHandBreak?.Invoke(value);

        protected abstract void FixedUpdate();
        protected virtual void Start()
        {
        }
    }
}
