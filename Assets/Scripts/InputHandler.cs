using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;

        public event Action LeftButtonClick;
        public event Action RightButtonClick;

        private void Start()
        {
            leftButton.onClick.AddListener(LeftClick);
            rightButton.onClick.AddListener(RightClick);
        }

        private void OnDestroy()
        {
            leftButton.onClick.RemoveListener(LeftClick);
            rightButton.onClick.RemoveListener(RightClick);
        }

        private void LeftClick()
        {
            LeftButtonClick?.Invoke();
        }

        private void RightClick()
        {
            RightButtonClick?.Invoke();
        }
    }
}