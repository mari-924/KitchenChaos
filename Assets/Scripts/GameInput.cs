using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
    public Vector2 GetMovementVectorNormalized()
    {
        //Gets the direction of where the player is moving
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        
        //Normalizes the vectors so the speed isn't too fast
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
