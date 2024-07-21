using Extensions;
using UnityEngine;

namespace Managers
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        [Header("Info - No Touch")]
        [SerializeField] private GameState currentGameState;
        [SerializeField] private GameState previousGameState;

        public GameState GetCurrentGameState() => currentGameState;
        public GameState GetPreviousGameState() => currentGameState;

        private void Start()
        {
            currentGameState = GameState.Free;
            previousGameState = GameState.Free;
        }

        public void ChangeGameState(GameState state)
        {
            previousGameState = currentGameState;
            currentGameState = state;
        }

        public void FailGame(string resourceName)
        {
            Time.timeScale = 0f;
            UIManager.Instance.ShowGameFailUI(resourceName);
        }
    }
}