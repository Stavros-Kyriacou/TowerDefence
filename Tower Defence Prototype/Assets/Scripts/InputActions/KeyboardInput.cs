// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputActions/Keyboard.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @KeyboardInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @KeyboardInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Keyboard"",
    ""maps"": [
        {
            ""name"": ""Keyboard"",
            ""id"": ""5a4fb17a-39d3-452d-8234-82fc035ec5ee"",
            ""actions"": [
                {
                    ""name"": ""Turret1"",
                    ""type"": ""Button"",
                    ""id"": ""c2fcae01-0941-4e53-9b40-f4892fb3d517"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turret2"",
                    ""type"": ""Button"",
                    ""id"": ""17bb7305-809a-48f3-85b4-2a164afa7ddb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turret3"",
                    ""type"": ""Button"",
                    ""id"": ""d4898fd9-d1ae-46fe-aace-4f2bb65d2df3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turret4"",
                    ""type"": ""Button"",
                    ""id"": ""0a539e11-92d5-417f-8a64-69e3af859ff8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turret5"",
                    ""type"": ""Button"",
                    ""id"": ""808162fc-5397-4da5-b8f1-a41e15e5298e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b866cad8-6cc9-458e-acfb-24546a703419"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turret1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b00209fa-0b98-477e-a4f7-50719df030d4"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turret2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1dc45445-bbb5-4011-82cb-a8e119b31f0d"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turret3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ac07c8f-cb15-46d7-bc6a-f22b6e6d4b8f"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turret4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9f2c727-3d07-4e5a-a255-a3f42a1a4e53"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turret5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_Turret1 = m_Keyboard.FindAction("Turret1", throwIfNotFound: true);
        m_Keyboard_Turret2 = m_Keyboard.FindAction("Turret2", throwIfNotFound: true);
        m_Keyboard_Turret3 = m_Keyboard.FindAction("Turret3", throwIfNotFound: true);
        m_Keyboard_Turret4 = m_Keyboard.FindAction("Turret4", throwIfNotFound: true);
        m_Keyboard_Turret5 = m_Keyboard.FindAction("Turret5", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_Turret1;
    private readonly InputAction m_Keyboard_Turret2;
    private readonly InputAction m_Keyboard_Turret3;
    private readonly InputAction m_Keyboard_Turret4;
    private readonly InputAction m_Keyboard_Turret5;
    public struct KeyboardActions
    {
        private @KeyboardInput m_Wrapper;
        public KeyboardActions(@KeyboardInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Turret1 => m_Wrapper.m_Keyboard_Turret1;
        public InputAction @Turret2 => m_Wrapper.m_Keyboard_Turret2;
        public InputAction @Turret3 => m_Wrapper.m_Keyboard_Turret3;
        public InputAction @Turret4 => m_Wrapper.m_Keyboard_Turret4;
        public InputAction @Turret5 => m_Wrapper.m_Keyboard_Turret5;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @Turret1.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTurret1;
                @Turret1.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTurret1;
                @Turret1.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTurret1;
                @Turret2.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTurret2;
                @Turret2.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTurret2;
                @Turret2.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTurret2;
                @Turret3.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTurret3;
                @Turret3.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTurret3;
                @Turret3.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTurret3;
                @Turret4.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTurret4;
                @Turret4.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTurret4;
                @Turret4.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTurret4;
                @Turret5.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTurret5;
                @Turret5.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTurret5;
                @Turret5.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTurret5;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Turret1.started += instance.OnTurret1;
                @Turret1.performed += instance.OnTurret1;
                @Turret1.canceled += instance.OnTurret1;
                @Turret2.started += instance.OnTurret2;
                @Turret2.performed += instance.OnTurret2;
                @Turret2.canceled += instance.OnTurret2;
                @Turret3.started += instance.OnTurret3;
                @Turret3.performed += instance.OnTurret3;
                @Turret3.canceled += instance.OnTurret3;
                @Turret4.started += instance.OnTurret4;
                @Turret4.performed += instance.OnTurret4;
                @Turret4.canceled += instance.OnTurret4;
                @Turret5.started += instance.OnTurret5;
                @Turret5.performed += instance.OnTurret5;
                @Turret5.canceled += instance.OnTurret5;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    public interface IKeyboardActions
    {
        void OnTurret1(InputAction.CallbackContext context);
        void OnTurret2(InputAction.CallbackContext context);
        void OnTurret3(InputAction.CallbackContext context);
        void OnTurret4(InputAction.CallbackContext context);
        void OnTurret5(InputAction.CallbackContext context);
    }
}
