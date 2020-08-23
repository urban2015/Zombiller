using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class ClosePanelButton : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GameObject panel;
        [SerializeField] private Button button;

        //Add listener to the Start Game Button
        private void Start() => button.onClick.AddListener(CloseStartPanel);
        
        /// <summary>
        /// Deactivates the selected panel.
        /// </summary>
        void CloseStartPanel() => panel.SetActive(false);
    }
}