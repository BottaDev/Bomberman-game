using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    [Header("Estadisticas")]
    public float movementSpeed = 100f;
    //[Range(0, 1)]
    //public float stepDuration = 0.5f;

    [Header("Movimiento")]
    public string inputAxisX;
    public string inputAxisY;

    private float auxAxisX;
    private float auxAxisY;
    private float accumulatedTravelTime;
    private MapController mapController;

    //private Coroutine playerMovement;

    private void Start()
    {
        mapController = GameObject.Find("Grid Map").GetComponent<MapController>();

        if (mapController == null)
        {
            Debug.Log("Error. No se encontró el objeto 'Grid Map'");
            return;
        }
    }

    //private void Update()
    //{
    //    if (playerMovement == null)
    //    {
    //        if (Input.GetKey(KeyCode.W))        
    //            playerMovement = StartCoroutine(Move(Vector2.up));
    //        else if (Input.GetKey(KeyCode.S))
    //            playerMovement = StartCoroutine(Move(Vector2.down));
    //        else if (Input.GetKey(KeyCode.D))
    //            playerMovement = StartCoroutine(Move(Vector2.right));
    //        else if (Input.GetKey(KeyCode.A))
    //            playerMovement = StartCoroutine(Move(Vector2.left));
    //    }
    //}

    //private IEnumerator Move(Vector2 direction)
    //{
    //    Vector2 startPosition = transform.position;
    //    Vector2 destinationPosition = (Vector2) transform.position + direction;
    //    float time = 0.0f;

    //    bool colDetected = Physics2D.Linecast(startPosition, destinationPosition, 8);   // Siempre devuelve false

    //    if (!colDetected)
    //    {
    //        while (time < 1.0f)
    //        {
    //            transform.position = Vector2.Lerp(startPosition, destinationPosition, time);
    //            time += Time.deltaTime / stepDuration;
    //            yield return new WaitForEndOfFrame();
    //        }

    //        transform.position = destinationPosition;
    //    }

    //    playerMovement = null;
    //}

    private void Update()
    {
        auxAxisX = Input.GetAxis(inputAxisX);
        auxAxisY = Input.GetAxis(inputAxisY);

        ProcessInput();
    }

    private void ProcessInput()
    {
        if (auxAxisX > 0)
            Move(transform.position, transform.position + new Vector3(1, 0, 0));    // Right
        else if (auxAxisX < 0)
            Move(transform.position, transform.position + new Vector3(-1, 0, 0));   // Left
        else if (auxAxisY > 0)
            Move(transform.position, transform.position + new Vector3(0, 1, 0));    // Up
        else if (auxAxisY < 0)
            Move(transform.position, transform.position + new Vector3(0, -1, 0));   // Down
    }

    private void Move(Vector3 from, Vector3 to)
    {
        StartCoroutine(MovePlayer(from, to));
    }

    private IEnumerator MovePlayer(Vector3 startCell, Vector3 endCell)
    {
        float totalDistance = (startCell - endCell).magnitude;
        float totalTravelTime = totalDistance / movementSpeed;

        float accumulatedTravelTime = 0f;
        
        while (accumulatedTravelTime < totalTravelTime)
        {
            transform.position = Vector3.Lerp(startCell, endCell, accumulatedTravelTime / totalTravelTime);
            totalTravelTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endCell;
    }
}
