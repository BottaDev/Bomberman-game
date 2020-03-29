using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Player player;
    public Rigidbody2D rb;

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
            rb.MovePosition(transform.position + new Vector3(0, 1, 0) * player.speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            rb.MovePosition(transform.position + new Vector3(0, -1, 0) * player.speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            rb.MovePosition(transform.position + new Vector3(-1, 0, 0) * player.speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            rb.MovePosition(transform.position + new Vector3(1, 0, 0) * player.speed * Time.deltaTime);
    }
}
