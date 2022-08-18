using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars
{
    public class BotTargetPoint : MonoBehaviour
    {
        void Start()
        {
            var render = GetComponent<MeshRenderer>();
            Destroy(render);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 2f);
        }

        public event System.Action<BotTargetPoint> OnTrigger;
        private void OnTriggerEnter(Collider other)
        {
            OnTrigger?.Invoke(this);
        }
    } 
}
