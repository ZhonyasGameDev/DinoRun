using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    private bool isJumping;
    [SerializeField] private bool isDead;
    [SerializeField] private float gravityScale = 2f;
    [SerializeField] private float fallGravityScale = 4f;

    private bool onGrounded;
    public event EventHandler OnIsDead;
    public event EventHandler<OnIsJumpingEventArgs> OnIsJumping;
    public class OnIsJumpingEventArgs : EventArgs
    {
        public bool isJumping;
    }
    private Rigidbody2D rb2D;
    [SerializeField] float minJumpForce;
    [SerializeField] float maxJumpForce;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("There is more than one Player Instance!");
        }
        Instance = this;

        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //As long the Player is not dead you can Play
        if (!isDead)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && onGrounded)
            {
                isJumping = true;
                onGrounded = false;

                OnIsJumping?.Invoke(this, new OnIsJumpingEventArgs
                {
                    isJumping = isJumping
                });

                float randomJumpForce = UnityEngine.Random.Range(minJumpForce, maxJumpForce);
                rb2D.AddForce(Vector2.up * randomJumpForce, ForceMode2D.Impulse);
            }

            if (rb2D.velocity.y > 0)
            {
                rb2D.gravityScale = gravityScale;
            }
            else
            {
                rb2D.gravityScale = fallGravityScale;
            }
        }

    }

    //if the Player is touching the floor the Player can Jump
    private void OnCollisionEnter2D(Collision2D other)
    {
        onGrounded = true;
        isJumping = false;

        OnIsJumping?.Invoke(this, new OnIsJumpingEventArgs
        {
            isJumping = isJumping
        });

    }

    // Check if the Player touches another object (cactus)
    private void OnTriggerEnter2D(Collider2D other)
    {
        rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        isDead = true;
        OnIsDead?.Invoke(this, EventArgs.Empty);
        // Debug.Log(other.name);
    }

    public bool IsJumping()
    {
        return isJumping;
    }

    public bool IsDead()
    {
        return isDead;
    }
}
