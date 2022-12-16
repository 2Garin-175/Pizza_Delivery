using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    private const int MAX_ROAD_IN_ROW = 3;
    private const int MAX_SAFE_ZONE_IN_ROW = 1;
    private const int TILE_WIDTH = 4;
    private const int DISTANCE_TO_PIZZA_POINT = 10;

    private Vector3 posicionToSpawn = new Vector3(-16, 0, 0);

    private int tileCount = 4;
    private int roadInRow = 0;
    private int SafeZoneInRow = 0;
    private int trigerPosicionToSpawnX = 0;

    private int tileArrayID = 5;
    [SerializeField] private GameObject[] tileData = new GameObject[11];

    [SerializeField] private GameObject[] safeZone;
    [SerializeField] private GameObject road;
    [SerializeField] private GameObject customer;

    [SerializeField] private Transform mainBicycle;

   

    private void Start()
    {
        SpawnStartTile();
    }

    private void FixedUpdate()
    {
        if(mainBicycle)
        if (mainBicycle.position.x < trigerPosicionToSpawnX)
        {
            trigerPosicionToSpawnX -= TILE_WIDTH;
            SpawnTile();
        }
    }

    void SpawnStartTile()
    {
        for (int i = 0; i < 4; i++)
        {
            SpawnTile();
        }
    }

    void SpawnTile()
    {
        tileCount++;

        if (tileCount == DISTANCE_TO_PIZZA_POINT)
        {
            tileCount = 0;
            SpawnPizzaPoint();
        }
        else
        {
            if (roadInRow >= MAX_ROAD_IN_ROW)
            {
                roadInRow = 0;
                SpawnSafeZone();
            }
            else if (SafeZoneInRow >= MAX_SAFE_ZONE_IN_ROW)
            {
                SafeZoneInRow = 0;
                SpawnRoad();
            }
            else
            {
                if (Random.Range(0, 2) == 0)
                {
                    SpawnRoad();
                }
                else
                {
                    SpawnSafeZone();
                }
            }
        }
        
        posicionToSpawn += new Vector3(-TILE_WIDTH, 0, 0);

        GameObject SpawnRoad()
        {
            GameObject _go = Instantiate(road, posicionToSpawn, Quaternion.Euler(0,0,0));
            tileData[DestroyOldTile()] = _go;

            roadInRow++;
            return _go;
        }

        GameObject SpawnSafeZone()
        {
            GameObject _go = Instantiate(safeZone[Random.Range(0, safeZone.Length)], posicionToSpawn, Quaternion.Euler(0, 0, 0));
            tileData[DestroyOldTile()] = _go;

            SafeZoneInRow++;
            return _go;
        }

        void SpawnPizzaPoint()
        {
            GameObject _customerZone = SpawnSafeZone();
            GameObject _customerGO = Instantiate(customer, _customerZone.transform);

            mainBicycle.GetComponent<Bicycle>().customerTargetPosicion = _customerZone.transform.position;
            mainBicycle.GetComponent<Bicycle>().getPizza = true;
           _customerGO.transform.localPosition = new Vector3(-0.6f,0.6f,1);
        }
    }

    int DestroyOldTile()
    {
        if (tileData[tileArrayID] != null)
        {
            Destroy(tileData[tileArrayID]);
        }

        int retID = tileArrayID;

        tileArrayID ++;
        if (tileArrayID > tileData.Length - 1)
            tileArrayID = 0;

        return retID;
    }
}
// 7