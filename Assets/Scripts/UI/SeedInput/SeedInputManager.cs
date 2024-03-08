using MarkUlrich.StateMachine;
using MarkUlrich.StateMachine.States;
using System.Threading.Tasks;
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
        [SerializeField] private Button _generateButton;
        [SerializeField] private Button _pasteButton;

        private string _wantedSeed;

        private void Start()
        {
            _inputField.onEndEdit.AddListener(GetSeed);
            _generateButton.onClick.AddListener(EnterSeed);
            _pasteButton.onClick.AddListener(PasteCode);
        }

        private void GetSeed(string givenSeed) => _wantedSeed = givenSeed;

        private void EnterSeed()
        {
            if (string.IsNullOrEmpty(_wantedSeed))
            {
                //make error appear
                return;
            }
            GameStateMachine.Instance.SetState<GameState>();
            UnloadMainMenu();
        }

        private async void UnloadMainMenu()
        {
            await Task.Delay(100);

            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            FindObjectOfType<GenerationMediator>().GenerateMaze(_wantedSeed);
        }

        private void PasteCode()
        {
            _wantedSeed = GUIUtility.systemCopyBuffer;
            _inputField.text = _wantedSeed;
        }
        
    }
}
