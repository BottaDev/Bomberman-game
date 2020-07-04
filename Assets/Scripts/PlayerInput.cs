using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInput : MonoBehaviour
{
    [Header("Movimiento")]
    public string inputAxisX;
    public string inputAxisY;

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
    private Player player;
    private Rigidbody2D rb;
    private MapController mapController;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        mapController = GameObject.Find("Grid Map").GetComponent<MapController>();

        if (mapController == null)
        {
            Debug.Log("Error. No se encontró el objeto 'Grid Map'");
            return;
        }
    }

    private void FixedUpdate()
    {
        auxAxisX = Input.GetAxis(inputAxisX);
        auxAxisY = Input.GetAxis(inputAxisY);

        ProcessInput();
    }

    void ProcessInput()
    {
        if (auxAxisY > 0)
        {
            rb.MovePosition(transform.position + new Vector3(0, 1, 0) * player.speed * Time.deltaTime);
            player.animator.SetFloat("MoveU", 1);
            player.animator.SetFloat("MoveD", 0);
            player.animator.SetFloat("MoveL", 0);
            player.animator.SetFloat("MoveR", 0);
            moveUp = true;
            moveDown = false;
            moveLeft = false;
            moveRight = false;
        }
        else if (auxAxisY < 0)
        {
            rb.MovePosition(transform.position + new Vector3(0, -1, 0) * player.speed * Time.deltaTime);
            player.animator.SetFloat("MoveD", 1);
            player.animator.SetFloat("MoveU", 0);
            player.animator.SetFloat("MoveL", 0);
            player.animator.SetFloat("MoveR", 0);
            moveUp = false;
            moveDown = true;
            moveLeft = false;
            moveRight = false;
        }
        else if(auxAxisX < 0)
        {
            rb.MovePosition(transform.position + new Vector3(-1, 0, 0) * player.speed * Time.deltaTime);
            player.animator.SetFloat("MoveL", 1);
            player.animator.SetFloat("MoveU", 0);
            player.animator.SetFloat("MoveD", 0);
            player.animator.SetFloat("MoveR", 0);
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            moveUp = false;
            moveDown = false;
            moveLeft = true;
            moveRight = false;
        }
        else if (auxAxisX > 0)
        {
            rb.MovePosition(transform.position + new Vector3(1, 0, 0) * player.speed * Time.deltaTime);
            player.animator.SetFloat("MoveR", 1);
            player.animator.SetFloat("MoveU", 0);
            player.animator.SetFloat("MoveD", 0);
            player.animator.SetFloat("MoveL", 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            moveUp = false;
            moveDown = false;
            moveLeft = false;
            moveRight = true;
        }
        else
        {
            player.animator.SetFloat("MoveR", 0);
            player.animator.SetFloat("MoveU", 0);
            player.animator.SetFloat("MoveD", 0);
            player.animator.SetFloat("MoveL", 0);
        }
    }
}