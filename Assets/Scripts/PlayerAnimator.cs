using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_JUMP = "isJump";
    private const string IS_DEAD = "doDie";
    private Animator animator;

    private void Start()
    {
        // Player.Instance.OnIsDead += Player_OnIsDie;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_JUMP, Player.Instance.IsJumping());
        animator.SetBool(IS_DEAD, Player.Instance.IsDead());
    }
/* 
    private void Player_OnIsDie(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_DEAD, Player.Instance.IsDead());
    } */

}
