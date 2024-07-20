using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private const int GRID_HEIGHT = 1;

    [SerializeField] private int _gridSizeX;
    [SerializeField] private int _gridSizeZ;
    [SerializeField] private int _maxItemsInGridCell;
    [SerializeField] private Transform _gridCellSelectVisual;

    int[,] myGrid;

    void PlaceObjectInGrid(Transform objectToBePlaced, int sizeX, int sizeZ, int cost)
    {
        for (int x = 0; x < myGrid.GetLength(0); x++)
        {
            for(int z = 0; z < myGrid.GetLength(1); z++)
            {
                if (z >= sizeZ && x >= sizeX && (myGrid[z, x] + cost) < _maxItemsInGridCell)
                {
                    Vector3 pos = new Vector3(x,this.transform.position.y, z);
                    Instantiate(objectToBePlaced, pos, this.transform.rotation, this.transform);
                    myGrid[z, x] += cost;
                }
            }
        }
    }
    void UpdateGridCellVisual()
    {
        Tuple<float, float> gridCoordinates = GetSelectedCell();

        if (gridCoordinates != null)
        {

            Vector3 newPos = new Vector3(gridCoordinates.Item1, 0f, gridCoordinates.Item2);

            _gridCellSelectVisual.position = newPos;
        }
    }
    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            return hitInfo.point - this.transform.position;
        }
        return Vector3.down;
    }
    private Tuple<float, float> GetSelectedCell()
    {
        Vector3 pos = GetMousePosition();
        if (pos != Vector3.down)
        {

            float i, j;

            i = Mathf.FloorToInt(pos.x);
            j = Mathf.FloorToInt(pos.z);

            return Tuple.Create(i, j);
        }
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(_gridSizeX, GRID_HEIGHT, _gridSizeZ);
        myGrid = new int[_gridSizeZ, _gridSizeX];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetSelectedCell();
        }
        UpdateGridCellVisual();
    }
}
