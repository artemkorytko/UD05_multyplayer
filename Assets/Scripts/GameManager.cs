using System;
using a;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        private Player _localPlayer;

        private InputHandler _inputHandler;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        private void Start()
        {
            _inputHandler = FindObjectOfType<InputHandler>();
            if (_inputHandler)
            {
                _inputHandler.LeftButtonClick += OnLeftButtonClick;
                _inputHandler.RightButtonClick += OnRightButtonClick;
            }
        }

       
        private void OnDestroy()
        {
            if (_inputHandler)
            {
                _inputHandler.LeftButtonClick -= OnLeftButtonClick;
                _inputHandler.RightButtonClick -= OnRightButtonClick;
            }
        }

        private void OnLeftButtonClick()
        {
            if (_localPlayer)
            {
                _localPlayer.MoveLeft();
            }
        }

        private void OnRightButtonClick()
        {
            if (_localPlayer)
            {
                _localPlayer.MoveRight();
            }
        }

        public void SetPlayer(Player player)
        {
            _localPlayer = player;
        }
    }
}