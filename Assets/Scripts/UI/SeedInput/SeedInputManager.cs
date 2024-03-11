using MarkUlrich.StateMachine;
using MarkUlrich.StateMachine.States;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UntitledCube.Maze.Generation;

namespace UntitledCube.UI.SeedInput
{
    public class SeedInputManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private GameObject _warningText;
        [SerializeField] private Button _generateButton;
        [SerializeField] private Button _pasteButton;

        private string _wantedSeed;

        private void Start()
        {
            _warningText.SetActive(false);

            _inputField.onEndEdit.AddListener(GetSeed);
            _generateButton.onClick.AddListener(EnterSeed);
            _pasteButton.onClick.AddListener(PasteCode);
        }

        private void OnEnable() => GameStateMachine.Instance.GetState<GameState>().OnSceneLoaded += UnloadMainMenu;

        private void OnDisable() => GameStateMachine.Instance.GetState<GameState>().OnSceneLoaded -= UnloadMainMenu;

        private void GetSeed(string givenSeed) => _wantedSeed = givenSeed;

        private void EnterSeed()
        {
            if (string.IsNullOrEmpty(_wantedSeed) || !SeedCodec.Validate(_wantedSeed))
            {
                _warningText.SetActive(true);
                return;
            }
            _warningText.SetActive(false);
            GameStateMachine.Instance.SetState<GameState>();
        }

        private void UnloadMainMenu()
        {
            FindObjectOfType<GenerationMediator>().GenerateMaze(_wantedSeed);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }

        private void PasteCode()
        {
            _wantedSeed = GUIUtility.systemCopyBuffer;
            _inputField.text = _wantedSeed;
        }

    }
}