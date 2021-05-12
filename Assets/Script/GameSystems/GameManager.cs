using UnityEngine;
using UnityEngine.SceneManagement;
namespace Project.GameSystem
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject levelObjects;

        #region delegates
        public delegate void OnGameStateChanged();

        public OnGameStateChanged onGameStart;
        public OnGameStateChanged onRaceFinished;
        public OnGameStateChanged onGameEnded;
        public OnGameStateChanged onPlayerFailed;
        #endregion

        public static GameManager instance;
        private void Awake()
        {
            instance = this;
        }
        public void Restart()
        {
            Debug.Log("Game Restarted");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void StartGame()
        {
            if (!levelObjects.activeSelf)
                levelObjects.SetActive(true);
            onGameStart?.Invoke();
        }
        public void FinishRace()
        {
            Debug.Log("Game Finished!");
            levelObjects.SetActive(false);
            onRaceFinished?.Invoke();
        }
        public void EndGame()
        {
            onGameEnded?.Invoke();
        }
        public void OnPlayerFailed()
        {
            onPlayerFailed?.Invoke();
        }
    }
}
