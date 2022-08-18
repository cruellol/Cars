using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars
{
    [RequireComponent(typeof(BaseInputController), typeof(WheelComponent), typeof(Rigidbody))]
    public class CarComponent : MonoBehaviour
    {
        public static bool CanControl;

        private BaseInputController _input;
        private WheelComponent _wheels;
        private Rigidbody _rigidBody;

        [SerializeField, Range(5f, 40f)]
        private float _maxSteerAngle = 25f;
        [SerializeField]
        private float _torque = 2500f;
        [SerializeField, Range(0f, float.MaxValue)]
        private float _handBrakeTorque = float.MaxValue;
        [SerializeField,Space]
        private Vector3 _centerOfMass;

        private void FixedUpdate()
        {
            if (!CanControl) return;
            _wheels.UpdateVisual(_input.Rotate * _maxSteerAngle);
            var torque = _input.Acceleration * _torque / 2f;
            foreach(var wheel in _wheels.GetRearColiders)
            {
                wheel.motorTorque = torque;
            }
        }

        private void OnHandBrake(bool value)
        {
            if (value)
            {
                foreach(var wheel in _wheels.GetRearColiders)
                {
                    wheel.brakeTorque = _handBrakeTorque;
                }
            }
            else
            {
                foreach (var wheel in _wheels.GetRearColiders)
                {
                    wheel.brakeTorque = 0f;
                }
            }
        }
        private void Start()
        {
            _wheels = GetComponent<WheelComponent>();
            _input = GetComponent<BaseInputController>();
            _rigidBody = GetComponent<Rigidbody>();
            _rigidBody.centerOfMass = _centerOfMass;

            _input.OnHandBreak += OnHandBrake;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.TransformPoint(_centerOfMass),0.5f);
        }

        private void OnDestroy()
        {
            _input.OnHandBreak -= OnHandBrake;
        }
    } 
}