using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    private const int ROAD_LENGTH = 1600;
    private const float CAR_SPEED = 0.035f;

    [SerializeField] private GameObject[] cars;

    [SerializeField] private Transform leftSpawnTarget;
    [SerializeField] private Transform rightSpawnTarget;

    private IEnumerator Start()
    {
        while (true)
        {
            SpawnCar();

            yield return new WaitForSecondsRealtime(Random.Range(6,8));
        }
    }

    void SpawnCar()
    {
        int side = Random.Range(0, 2);
        Transform spawnPoint = side == 0 ? leftSpawnTarget : rightSpawnTarget;

        GameObject car = Instantiate(cars[Random.Range(0,cars.Length)], this.transform);
        car.transform.position = spawnPoint.position;

        if(spawnPoint == rightSpawnTarget)
        {
            car.transform.rotation = Quaternion.Euler(0,180,0);
        }

        StartCoroutine(MoveCar(car, side));
    }

    IEnumerator MoveCar(GameObject car, int side)
    {
        for (int i = 0; i < ROAD_LENGTH; i ++)
        {
            car.transform.position += side == 0 ? new Vector3(0, 0, CAR_SPEED) : new Vector3(0, 0, -CAR_SPEED);
            yield return new WaitForFixedUpdate();
        }
        Destroy(car);
    }
}
