using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cars
{
    public class Speedometer : MonoBehaviour
    {
        private const float c_convertMeterInSecFromKmInH = 3.6f;
        private Transform _target;

        [SerializeField]
        private float _minSpeed = 0f;
        [SerializeField]
        private float _maxSpeed = 300f;

        [SerializeField, Space]
        private Color _minColor;
        [SerializeField]
        private Color _maxColor;

        [SerializeField, Space, Range(0.1f, 1f)]
        private float _delay = 0.3f;
        [SerializeField]
        private Text _text;

        private void Start()
        {
            _target = FindObjectOfType<PlayerInputController>().transform;
            StartCoroutine(Speed());
        }

        private IEnumerator Speed()
        {
            var prevPos = _target.position;
            while (true)
            {
                var distance = Vector3.Distance(prevPos, _target.position);
                var speed = Mathf.Round(distance / _delay * c_convertMeterInSecFromKmInH);

                _text.color = Color.Lerp(_minColor, _maxColor, speed / _maxSpeed);
                _text.text = speed.ToString();
                prevPos = _target.position;
                yield return new WaitForSeconds(_delay);
            }
        }
    }
}