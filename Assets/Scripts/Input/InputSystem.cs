using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;
using System;

namespace UntitledCube.Input 
{
    public static class InputSystem
    {
        private readonly static InputActions _inputAction = new();
        private readonly static Dictionary<Action<InputAction.CallbackContext>, InputAction> _subscribedInputs = new();

        /// <summary>
        /// Enables or Disables Input based on isActive
        /// </summary>
        public static void ToggleAllInputs(bool isActive) 
        {
            List<InputAction> actions = _subscribedInputs.Values.ToList();

            foreach (InputAction action in actions)
            {
                if (isActive)
                    action.Enable();
                else 
                    action.Disable();
            }
        }

        /// <summary>
        /// Tries to get an action based on the given name
        /// </summary>
        /// <param name="actionName"> The name of the action you want </param>
        /// <returns>Returns the input action you are looking for if it exists </returns>
        public static InputAction TryGetAction(string actionName) => _inputAction.FindAction(actionName);

        /// <summary>
        /// Enables the given action and add the function to the preformed
        /// </summary>
        /// <param name="inputAction"> The action you want to subscribe to</param>
        /// <param name="function"> The function you want to have subscribed</param>
        public static void SubscribeToAction(InputAction inputAction, Action<InputAction.CallbackContext> function)
        {
            inputAction.Enable();
            inputAction.performed += function;

            _subscribedInputs.Add(function, inputAction);
        }

        /// <summary>
        /// Searches for the input based on the inputName, enables it and add the function to the preformed
        /// </summary>
        /// <param name="inputName">The name of the action you want</param>
        /// <param name="function"> The function you want to have subscribed</param>
        /// <param name="inputAction"> Gives back the action it subsribed to</param>
        public static void SubscribeToAction(string inputName, Action<InputAction.CallbackContext> function, out InputAction inputAction)
        {
            inputAction = TryGetAction(inputName);
            inputAction.Enable();
            inputAction.performed += function;

            _subscribedInputs.Add(function, inputAction);
        }


        /// <summary>
        /// Removes the function from the preformed of the given action
        /// </summary>
        /// <param name="inputAction"> The action you want to unsubscribe to</param>
        /// <param name="function"> The function you want to have unsubscribed</param>
        public static void UnsubscribeToAction(InputAction inputAction, Action<InputAction.CallbackContext> function)
        {
            inputAction.performed -= function;

            _subscribedInputs.Remove(function, out inputAction);
        }

        /// <summary>
        /// Searches for the input based on the inputName and removes the function from the preformed
        /// </summary>
        /// <param name="inputName">The name of the action you want</param>
        /// <param name="function"> The function you want to have unsubscribed</param>
        public static void UnsubscribeToAction(string inputName, Action<InputAction.CallbackContext> function)
        {
            InputAction inputAction = TryGetAction(inputName);
            inputAction.performed -= function;

            _subscribedInputs.Remove(function, out inputAction);
        }

        /// <summary>
        /// Unsubscribes each subscribed function from their actions
        /// </summary>
        public static void UnsubscribeToAllActions()
        {
            foreach (KeyValuePair<Action<InputAction.CallbackContext>, InputAction> input in _subscribedInputs)
                input.Value.performed -= input.Key;

            _subscribedInputs.Clear();
        }
    }
}
