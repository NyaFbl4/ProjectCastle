using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private EGameState _gameState;

        [SerializeField] private readonly List<IGameListener> _gameListeners = new();
        [SerializeField] private readonly List<IGameUpdateListener> _gameUpdateListeners = new();
        [SerializeField] private readonly List<IGameFixedUpdateListener> _gameFixedUpdateListeners = new();

        private void Awake()
        {
            _gameState = EGameState.Off;

            IGameListener.onRegister += AddListener;
            IGameListener.onUnregister += RemoveListener;
        }

        private void Start()
        {
            StartGame();
        }


        private void OnDestroy()
        {
            _gameState = EGameState.Finish;
            
            IGameListener.onRegister -= AddListener;
            IGameListener.onUnregister -= RemoveListener;
        }

        private void Update()
        {
            if (_gameState != EGameState.Play)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            for (int i = 0; i < _gameUpdateListeners.Count; i++)
            {
                _gameUpdateListeners[i].OnUpdate(deltaTime);
                //Debug.Log("GM Update");
            }
        }

        private void FixedUpdate()
        {
            if (_gameState != EGameState.Play)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            for (int i = 0; i < _gameFixedUpdateListeners.Count; i++)
            {
                _gameFixedUpdateListeners[i].OnFixedUpdate(deltaTime);
                //Debug.Log("GM FixedUpdate");
            }
        }

        private void AddListener(IGameListener gameListener)
        {
            _gameListeners.Add(gameListener);

            if (gameListener is IGameUpdateListener gameUpdateListener)
            {
                _gameUpdateListeners.Add(gameUpdateListener);
            }

            if (gameListener is IGameFixedUpdateListener gameFixedUpdateListener)
            {
                _gameFixedUpdateListeners.Add(gameFixedUpdateListener);
            }
            
            //Debug.Log("AddListener");
        }

        private void RemoveListener(IGameListener gameListener)
        {
            _gameListeners.Remove(gameListener);
            
            if (gameListener is IGameUpdateListener gameUpdateListener)
            {
                _gameUpdateListeners.Remove(gameUpdateListener);
            }

            if (gameListener is IGameFixedUpdateListener gameFixedUpdateListener)
            {
                _gameFixedUpdateListeners.Remove(gameFixedUpdateListener);
            }
            
            //Debug.Log("RemoveListener");
        }

        [Button]
        public void StartGame()
        {
            foreach (var gameListener in _gameListeners)
            {
                //Debug.Log("IGameStartListener");
                if (gameListener is IGameStartListener gameStartListener)
                {
                    gameStartListener.OnStartGame();
                    //Debug.Log("IGameStartListener");
                }
            }

            _gameState = EGameState.Play;
            Time.timeScale = 1;
            Debug.Log("START GAME");
        }

        [Button]
        public void FinishGame()
        {
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameFinishListener gameFinishListener)
                {
                    gameFinishListener.OnFinishGame();
                }
            }
            
            Time.timeScale = 0;
            _gameState = EGameState.Finish;
            Debug.Log("FINISH");
        }
        
        [Button]
        public void PauseGame()
        {
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGamePauseListener gamePauseListener)
                {
                    gamePauseListener.OnPauseGame();
                }
            }
            
            Time.timeScale = 0;
            _gameState = EGameState.Pause;
            Debug.Log("PAUSE");
        }
        
        [Button]
        public void ResumeGame()
        {
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameResumeListener gameResumeListener)
                {
                    gameResumeListener.OnResumeGame();
                }
            }
            
            Time.timeScale = 1;
            _gameState = EGameState.Play;
            Debug.Log("RESUME");
       
        }
        
    }
}