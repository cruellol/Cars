using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Cars
{
    public class Finish : MonoBehaviour
    {
        [SerializeField]
        private Text _timer;
        [SerializeField]
        private Text _onStart;
        [SerializeField]
        private float _timeBeforeStart = 5f;
        private DateTime _start;
        private void Start()
        {
            _timer.enabled = false;
            _onStart.enabled = true;
            CarComponent.CanControl = false;
            StartCoroutine(ToStart(_timeBeforeStart));
        }

        IEnumerator ToStart(float time)
        {
            while (time > 0)
            {
                _onStart.text = time.ToString();
                yield return new WaitForSeconds(1);
                time--;
            }
            _onStart.enabled = false;
            _start = DateTime.Now;
            _timer.enabled = true;
            CarComponent.CanControl = true;
        }

        TimeSpan RaceTime => DateTime.Now.Subtract(_start);

        private void Update()
        {
            _timer.text = RaceTime.ToString(@"mm\:ss\:f");
        }
        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponentInParent<PlayerInputController>();
            if (player != null)
            {
                Score s = new Score(Ui.CurrentPlayer, RaceTime);
                Ui.AllScores.Add(s);
                var serialized = JsonConvert.SerializeObject(Ui.AllScores);
                using (StreamWriter writer = new StreamWriter(Ui.FilePath))
                {
                    writer.WriteLine(serialized);
                }
                SceneManager.LoadScene(sceneName: "Menu");
            }
        }
    }
}
