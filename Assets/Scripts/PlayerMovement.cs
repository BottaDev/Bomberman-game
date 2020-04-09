using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Player player;
    Rigidbody2D rb;
    public bool moveUp;
    public bool moveDown;
    public bool moveLeft;
    public bool moveRight;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.MovePosition(transform.position + new Vector3(0, 1, 0) * player.speed * Time.deltaTime);
            moveUp = true;
            moveDown = false;
            moveLeft = false;
            moveRight = false;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.MovePosition(transform.position + new Vector3(0, -1, 0) * player.speed * Time.deltaTime);
            moveUp = false;
            moveDown = true;
            moveLeft = false;
            moveRight = false;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.MovePosition(transform.position + new Vector3(-1, 0, 0) * player.speed * Time.deltaTime);
            moveUp = false;
            moveDown = false;
            moveLeft = true;
            moveRight = false;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.MovePosition(transform.position + new Vector3(1, 0, 0) * player.speed * Time.deltaTime);
            moveUp = false;
            moveDown = false;
            moveLeft = false;
            moveRight = true;
        }
    }
}
