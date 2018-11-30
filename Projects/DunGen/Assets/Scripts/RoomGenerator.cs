using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomGenerator : MonoBehaviour
{

    [SerializeField] [Range(0,200)] int numObjects = 25;
    float radius = 25;

    public GameObject roomTemplate;
    public List<GameObject> allRooms;
    public List<Vector3> roomPositions;

    public List<GameObject> mainRooms;
    public List<GameObject> roomsToDelete;

    void Awake()
    {
        allRooms = new List<GameObject>(new GameObject[numObjects]);
        roomPositions = new List<Vector3>(new Vector3[numObjects]);
        GenerateRooms();
    }

    public Vector3 GetRandomPoint()
    {
        Vector3 ret = new Vector3();

        ret = Random.insideUnitCircle;
        ret *= radius;

        return ret;
    }

    void GenerateRooms()
    {
        float xSum = 0;
        float zSum = 0;
        for (int i = 0; i < numObjects; i++)
        {
            if (allRooms != null)
            {
                int tempX = (Random.Range(2, 10));
                int tempY = (Random.Range(2, 10));
                roomTemplate.GetComponent<GridGenerator>().gridSize.x = tempX;
                roomTemplate.GetComponent<GridGenerator>().gridSize.y = tempY;
                allRooms[i] = Instantiate(roomTemplate);
                allRooms[i].transform.parent = transform;
               
                Vector3 randomPoint = GetRandomPoint();
                allRooms[i].transform.position = new Vector3(randomPoint.x, 0, randomPoint.y);
                roomPositions[i] = allRooms[i].transform.position;

                zSum += allRooms[i].GetComponent<GridGenerator>().gridSize.y;
                xSum += allRooms[i].GetComponent<GridGenerator>().gridSize.x;
            }
        }
        float xMean = (xSum / numObjects) * 0.75f;
        float zMean = (zSum / numObjects) * 0.75f;
        for (int i = 0; i < numObjects; i++)
        {
            bool biggerThanAverage = allRooms[i].GetComponent<GridGenerator>().gridSize.x >= xMean && allRooms[i].GetComponent<GridGenerator>().gridSize.y >= zMean;
            if (biggerThanAverage)
            {
                mainRooms.Add(allRooms[i]);
            }
            else
            {
                roomsToDelete.Add(allRooms[i]);
            }
        }
    }
}



