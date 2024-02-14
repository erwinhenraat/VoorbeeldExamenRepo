using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

namespace Input 
{
    public static class InputSystem
    {
        private static InputActions _inputAction = new();
        private static Dictionary<System.Action<InputAction.CallbackContext>, InputAction> _subscribedInputs = new();

        public static InputAction TryGetAction(string actionName)
        {
            InputAction action = _inputAction.FindAction(actionName);
            
            return action;
        }

        public static void SubscribeToAction(InputAction inputAction, System.Action<InputAction.CallbackContext> function)
        {
            inputAction.Enable();
            inputAction.performed += function;

            _subscribedInputs.Add(function, inputAction);
        }

        public static void SubscribeToAction(string inputName, System.Action<InputAction.CallbackContext> function, out InputAction inputAction)
        {
            inputAction = TryGetAction(inputName);
            inputAction.Enable();
            inputAction.performed += function;

            _subscribedInputs.Add(function, inputAction);
        }

        public static void UnsubscribeToAction(InputAction inputAction, System.Action<InputAction.CallbackContext> function)
        {
            inputAction.performed -= function;

            _subscribedInputs.Remove(function, out inputAction);
        }

        public static void UnsubscribeToAction(string inputName, System.Action<InputAction.CallbackContext> function)
        {
            InputAction inputAction = TryGetAction(inputName);
            inputAction.performed -= function;

            _subscribedInputs.Remove(function, out inputAction);
        }

        public static void UnsubscribeToAllActions()
        {
            foreach (KeyValuePair<System.Action<InputAction.CallbackContext>, InputAction> input in _subscribedInputs)
            {
                input.Value.performed -= input.Key;
            }

            _subscribedInputs.Clear();
        }
    }
}
