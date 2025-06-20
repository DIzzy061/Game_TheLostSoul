using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public PlayerInputActions PlayerInputActions { get; private set; }

    private void Awake()
    {
        Instance = this;
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Enable();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = PlayerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
}