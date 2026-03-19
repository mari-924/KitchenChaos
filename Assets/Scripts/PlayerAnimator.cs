using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    private Animator animator;
    private const string IS_WALKING = "IsWalking";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    
    private void Update()
    {
        //Checks if the player is moving so it can switch to the walking animation
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
