using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class GamepadNavegation : MonoBehaviour
{
    public GameObject botaoInicial;

    void OnEnable() {
        Navegacao();

        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    Navegacao();
                    break;
                case InputDeviceChange.Reconnected:
                    Navegacao();
                    break;
                default:
                    // See InputDeviceChange reference for other event types.
                    break;
            }
        };
    }

    void Navegacao() {
        if (Gamepad.current != null) {
            FindObjectOfType<EventSystem>().SetSelectedGameObject(botaoInicial);
        }
    }
}
