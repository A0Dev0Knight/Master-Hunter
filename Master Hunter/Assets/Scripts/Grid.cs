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

    void UpdateGridCellVisual()
    {
        Tuple<float, float> gridCoordinates = GetSelectedCell();

        Vector3 newPos = new Vector3(gridCoordinates.Item1,1,gridCoordinates.Item2);
        _gridCellSelectVisual.position = newPos;
    }
    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            return hitInfo.point - this.transform.position;
        }
        return this.transform.position;
    }
    private Tuple<float, float> GetSelectedCell()
    {
        Vector3 pos = GetMousePosition();
        float i = 0, j = 0;
        
        if (Input.GetMouseButtonDown(0))
        {
            i = Mathf.FloorToInt(pos.x);
            j = Mathf.FloorToInt(pos.z);
        }

        return Tuple.Create(i, j);
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
        GetSelectedCell();
        UpdateGridCellVisual();
    }
}
