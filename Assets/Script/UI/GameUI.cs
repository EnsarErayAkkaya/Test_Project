using Project.GameSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private GameObject play_Button;
        [SerializeField] private GameObject restart_Button;
        [SerializeField] private GameObject endGamePanel;
        [SerializeField] private GameObject failurePanel;
        [SerializeField] private Image paintingFillPercentImage;
        [SerializeField] private GameObject paintingCanvas;
        [SerializeField] private GameObject[] countdownObjects;
        [SerializeField] private float countdownDuration;

        private void Start()
        {
            GameManager.instance.onGameStart += CallCountdownEnumeration;
            GameManager.instance.onGameEnded += OnGameEnded;
            GameManager.instance.onPlayerFailed += OnPlayerFailed;
            GameManager.instance.onRaceFinished += OnRaceFinished;
        }

        public void OnPlayButtonPressed()
        {
            GameManager.instance.StartGame();
            play_Button.SetActive(false);
        }

        public void OnRestartButtonPressed()
        {
            GameManager.instance.Restart();
            endGamePanel.SetActive(false);
            failurePanel.SetActive(false);
            paintingCanvas.SetActive(false);
            restart_Button.SetActive(false);
            play_Button.SetActive(true);
        }
        private void OnGameEnded()
        {
            endGamePanel.SetActive(true);
            restart_Button.SetActive(true);
        }
        private void OnPlayerFailed()
        {
            failurePanel.SetActive(true);
            restart_Button.SetActive(true);
        }

        private void OnRaceFinished()
        {
            paintingCanvas.SetActive(true);
        }

        public void SetFillPercentImage(float percent)
        {
            paintingFillPercentImage.fillAmount = percent;
        }

        private void CallCountdownEnumeration() => StartCoroutine(CountdownEnumerator());
        private IEnumerator CountdownEnumerator()
        {
            int t = -1;
            while(t < countdownDuration )
            {
                t += 1;
                if (t > 0)
                    countdownObjects[t-1].SetActive(false);
                countdownObjects[t].SetActive(true);

                yield return new WaitForSeconds(1);
            }
            countdownObjects[t].SetActive(false);
        }
    }
}