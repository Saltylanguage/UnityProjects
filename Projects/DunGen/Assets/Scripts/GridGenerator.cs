using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] Transform tilePrefab;
    public Vector2 gridSize;

    [Range(0, 1)]
    [SerializeField] float outLinePercent = 0.9f;
  
    private void Awake()
    {
        CreateGrid();
    }

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-gridSize.x / 2 + x + 0.5f, 0, -gridSize.y / 2 + 0.5f + y);
    }
    public void CreateGrid()
    {

        string holderName = "Grid";
        if (transform.Find(holderName))
        {
            var temp = transform.Find(holderName);
            DestroyImmediate(temp.gameObject);
        }
        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.transform.parent = transform;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 tilePosition = CoordToPosition(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newTile.localScale = Vector3.one * (1 - outLinePercent);
                newTile.parent = mapHolder;
                BoxCollider box = GetComponent<BoxCollider>();
                box.size = new Vector3(gridSize.x * (1 - outLinePercent), 1, gridSize.y * (1 - outLinePercent));
            }
        }
    }
    public void AlignGrid(Vector3 cellDimensions)
    {
        float x = Mathf.Floor(transform.position.x / cellDimensions.x) * cellDimensions.x;
        float y = Mathf.Floor(transform.position.y / cellDimensions.y) * cellDimensions.y;
        float z = Mathf.Floor(transform.position.z / cellDimensions.z) * cellDimensions.z;

        transform.position = new Vector3(x, y, z);
    }


}
