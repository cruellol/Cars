using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cars
{
    public class WheelComponent : MonoBehaviour
    {
        private Transform[] _frontWheels;
        private Transform[] _rearWheels;
        private Transform[] _allWheels;

        private WheelCollider[] _frontColliders;
        private WheelCollider[] _rearColliders;
        private WheelCollider[] _allColliders;

        [SerializeField]
        private Transform _leftFrontWheel;
        [SerializeField]
        private Transform _rightFrontWheel;
        [SerializeField]
        private Transform _leftRearWheel;
        [SerializeField]
        private Transform _rightRearWheel;

        [SerializeField, Space]
        private WheelCollider _leftFrontCollider;
        [SerializeField]
        private WheelCollider _rightFrontCollider;
        [SerializeField]
        private WheelCollider _leftRearCollider;
        [SerializeField]
        private WheelCollider _rightRearCollider;

        public WheelCollider[] GetFrontColiders => _frontColliders;
        public WheelCollider[] GetRearColiders => _rearColliders;
        public WheelCollider[] GetAllColiders => _allColliders;

        public void UpdateVisual(float angle)
        {
            for(int i=0;i<2;i++)
            {
                _frontColliders[i].steerAngle = angle;
                _frontColliders[i].GetWorldPose(out var pos, out var rot);
                _frontWheels[i].SetPositionAndRotation(pos, rot);

                _rearColliders[i].GetWorldPose(out pos, out rot);
                _rearWheels[i].SetPositionAndRotation(pos, rot);
            }
        }

        private void Start()
        {
            _frontWheels = new Transform[] { _leftFrontWheel, _rightFrontWheel };
            _rearWheels = new Transform[] { _leftRearWheel, _rightRearWheel };
            _allWheels = new Transform[] { _leftFrontWheel, _rightFrontWheel, _leftRearWheel, _rightRearWheel };

            _frontColliders = new WheelCollider[] { _leftFrontCollider, _rightFrontCollider };
            _rearColliders = new WheelCollider[] { _leftRearCollider, _rightRearCollider };
            _allColliders = new WheelCollider[] { _leftFrontCollider, _rightFrontCollider, _leftRearCollider, _rightRearCollider };
        }
    }
}
