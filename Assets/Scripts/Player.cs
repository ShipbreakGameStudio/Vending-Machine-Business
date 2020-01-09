using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum PlayerState
{
    walk,
    interact,
}

public class Player : MonoBehaviour
{

    public float moveSpeed = 2.5f;

    Rigidbody2D rb;

    Vector2 movement;

    public PlayerState currentState;

    Animator anim;

    public Inventory playerInventory;

    public TextMeshProUGUI coinsText; 

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CoinHandler();
        StateHandler();
        MoveHandler();
    }

    public void MoveHandler()
    {
        if (currentState != PlayerState.interact)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            anim.SetFloat("walkY", movement.y);

            if (movement.x != 0 || movement.y != 0)
            {
                currentState = PlayerState.walk;
            }
        }
    }

    public void CoinHandler()
    {
        coinsText.text = playerInventory.money.ToString();
    }

    public void StateHandler()
    {
        if (currentState == PlayerState.interact)
        {
            moveSpeed = 0f;
        }
        if(currentState == PlayerState.walk)
        {
            moveSpeed = 2.5f;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
