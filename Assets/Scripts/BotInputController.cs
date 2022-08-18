using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cars
{
    public class BotInputController : BaseInputController
    {
        private int _index;
        [SerializeField]
        private List<BotTargetPoint> _points;

        private void Start()
        {
            base.Start();
            foreach(var point in _points)
            {
                point.OnTrigger += Point_OnTrigger;
            }
        }

        private void Point_OnTrigger(BotTargetPoint obj)
        {
            var index=_points.IndexOf(obj);
            if (index > _index)
            {
                _index = index;
            }
        }

        protected override void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        private float GetAngle()
        {
            var targetPos = _points[_index].transform.position;
            targetPos.y = transform.position.y;
            var direction = targetPos - transform.position;

            return Vector3.SignedAngle(direction, transform.forward, Vector3.up);
        }
    }
}
