using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }
    public static Player Instance{get; private set;}

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("There is more than 1 player Instance");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }
    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        //Gets the normalized vector of where the Player is wants to move
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        //Moves the player based on the vector
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float playerRadius = .7f, playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up*playerHeight
                        , playerRadius, moveDir, moveDistance);


        //Cannot move towards the moveDir
        if (!canMove)
        {
            //Attempt to only move along the X
           Vector3 moveDirX = new Vector3(moveDir.x, 0 , 0).normalized;
           canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up*playerHeight
                        , playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //Can move only on the X
                moveDir = moveDirX;
            }else
            {
                Vector3 moveDirZ = new Vector3(moveDir.x, 0 , 0).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up*playerHeight
                        , playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    //Can move only on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move in any direction
                }
            }

        }

        if (canMove)
        {
            transform.position += moveDir * moveSpeed *Time.deltaTime;
        }
        
        //Checks if the player is moving for the animation
        isWalking = moveDir != Vector3.zero;

        //Makes it so the player looks at the direction it is moving
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
    private void HandleInteraction()
    {
         //Gets the normalized vector of where the Player is wants to move
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        //Moves the player based on the vector
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if(moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //clearCounter.Interact();
                if(clearCounter != selectedCounter)
                {
                    selectedCounter = clearCounter;
                    SetSelectedCounter(selectedCounter);
                }
        
            }
            else
            {
                selectedCounter = null;

                SetSelectedCounter(null);
            }
        }else
        {
            selectedCounter = null;

            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
                    {
                        selectedCounter = selectedCounter
                    });
    }
}
