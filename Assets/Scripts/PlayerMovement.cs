using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string inputAxisX;
    public string inputAxisY;

    Player player;
    Rigidbody2D rb;

    [HideInInspector]
    public bool moveUp;
    [HideInInspector]
    public bool moveDown;
    [HideInInspector]
    public bool moveLeft;
    [HideInInspector]
    public bool moveRight;

    private float auxAxisX;
    private float auxAxisY;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        auxAxisX = Input.GetAxis(inputAxisX);
        auxAxisY = Input.GetAxis(inputAxisY);

        Move();
    }

    void Move()
    {
        if (auxAxisY > 0)
        {
            rb.MovePosition(transform.position + new Vector3(0, 1, 0) * player.speed * Time.deltaTime);
            moveUp = true;
            moveDown = false;
            moveLeft = false;
            moveRight = false;
        }
        if (auxAxisY < 0)
        {
            rb.MovePosition(transform.position + new Vector3(0, -1, 0) * player.speed * Time.deltaTime);
            moveUp = false;
            moveDown = true;
            moveLeft = false;
            moveRight = false;
        }
        if (auxAxisX < 0)
        {
            rb.MovePosition(transform.position + new Vector3(-1, 0, 0) * player.speed * Time.deltaTime);
            moveUp = false;
            moveDown = false;
            moveLeft = true;
            moveRight = false;
        }
        if (auxAxisX > 0)
        {
            rb.MovePosition(transform.position + new Vector3(1, 0, 0) * player.speed * Time.deltaTime);
            moveUp = false;
            moveDown = false;
            moveLeft = false;
            moveRight = true;
        }
    }
}
