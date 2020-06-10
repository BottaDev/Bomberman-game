using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    [Header("Estadisticas")]
    public float speed;

    [Header("Movimiento")]
    public string inputAxisX;
    public string inputAxisY;

    private float auxAxisX;
    private float auxAxisY;
    private float accumulatedTravelTime;
    private MapController mapController;

    private void Start()
    {
        mapController = GameObject.Find("Grid Map").GetComponent<MapController>();

        if (mapController == null)
        {
            Debug.Log("Error. No se encontró el objeto 'Grid Map'");
            return;
        }
    }

    private void Update()
    {
        auxAxisX = Input.GetAxis(inputAxisX);
        auxAxisY = Input.GetAxis(inputAxisY);

        ProcessInput();
    }

    private void ProcessInput()
    {
        if (auxAxisX > 0)
            StartCoroutine(MovePLayer(new Vector3Int(1, 0, 0)));    // Right
        else if (auxAxisX < 0)
            StartCoroutine(MovePLayer(new Vector3Int(-1, 0, 0)));   // Left
        else if (auxAxisY > 0)
            StartCoroutine(MovePLayer(new Vector3Int(0, 1, 0)));    // Up
        else if (auxAxisY < 0)
            StartCoroutine(MovePLayer(new Vector3Int(0, -1, 0)));   // Down
    }

    private IEnumerator MovePLayer(Vector3Int direction)
    {
        Vector3Int currentCell = mapController.GetCell(transform.position);
        Vector3Int targetCell = mapController.GetCell(transform.position + direction);

        float distance = Vector3.Distance(currentCell, targetCell);
        float totalTravelTime = distance / speed;

        // accumulatedTravelTime no debe sobrepasar totalTravelTime
        accumulatedTravelTime += Time.deltaTime;
        if (accumulatedTravelTime > totalTravelTime)
            accumulatedTravelTime = totalTravelTime;

        yield return new WaitForEndOfFrame();

        transform.position = Vector3.Lerp(currentCell, targetCell, accumulatedTravelTime / totalTravelTime);
    }
}
