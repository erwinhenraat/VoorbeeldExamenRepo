using Input.Settings;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

namespace Input 
{
    public class InputSystem5
    {
        private InputActions _inputAction;
        private Dictionary<System.Action<InputAction.CallbackContext>, InputAction> _subscribedInputs = new();

        public void InitializeInputSystem() => _inputAction = Addressables.LoadAssetAsync<InputActions>("InputActions").Result;

        public InputAction TryGetAction(string actionName)
        {
            InputAction action = _inputAction.FindAction(actionName);

            return action;
        }

        public void SubscribeToAction(InputAction inputAction, System.Action<InputAction.CallbackContext> function)
        {
            inputAction.Enable();
            inputAction.performed += function;

            _subscribedInputs.Add(function, inputAction);
        }

        public void SubscribeToAction(string inputName, System.Action<InputAction.CallbackContext> function, out InputAction inputAction)
        {
            inputAction = TryGetAction(inputName);
            inputAction.Enable();
            inputAction.performed += function;

            _subscribedInputs.Add(function, inputAction);
        }

        public void UnsubscribeToAction(InputAction inputAction, System.Action<InputAction.CallbackContext> function)
        {
            inputAction.performed -= function;

            _subscribedInputs.Remove(function, out inputAction);
        }

        public void UnsubscribeToAction(string inputName, System.Action<InputAction.CallbackContext> function)
        {
            InputAction inputAction = TryGetAction(inputName);
            inputAction.performed -= function;

            _subscribedInputs.Remove(function, out inputAction);
        }

        public void UnsubscribeToAllActions()
        {
            foreach (KeyValuePair<System.Action<InputAction.CallbackContext>, InputAction> input in _subscribedInputs)
            {
                input.Value.performed -= input.Key;
            }

            _subscribedInputs.Clear();
        }
    }
}
