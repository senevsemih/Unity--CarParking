using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public GameObject[] parkPrefabs;
    public GameObject powerUpPrefab;
    public List<GameObject> parkPoints;
    public GameObject[] startPoints;

    private float spawnRangeX = 10f;
    private float spawnRangeZmin = -4f;
    private float spawnRangeZmax = 50;

    public int carCount;
    public int waveNumber;

    void Start()
    {
        parkPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("ParkSpawnPoints"));
    }
    void Update()
    {
        carCount = GameObject.FindGameObjectsWithTag("Car").Length;
        SpawnPrefabs();
    }
    void SpawnPrefabs()
    {
        int randomCar = Random.Range(0, carPrefabs.Length);
        int randomCarSpot = Random.Range(0, startPoints.Length);
        int randomPark = Random.Range(0, parkPrefabs.Length);
        int randomParkSpot = Random.Range(0, parkPoints.Count);

        if (carCount == 0)
        {
            Instantiate(carPrefabs[randomCar], startPoints[randomCarSpot].transform.position, startPoints[randomCarSpot].transform.rotation);
            Instantiate(parkPrefabs[randomPark], parkPoints[randomParkSpot].transform.position, parkPoints[randomParkSpot].transform.rotation);
            parkPoints.RemoveAt(randomParkSpot);
            carCount++;
            waveNumber++;
            CarController.FindObjectOfType<CarController>().isStun = true;
        }
        if (ParkControler.FindObjectOfType<ParkControler>().hasPark == true && carCount == waveNumber)
        {
            Instantiate(carPrefabs[randomCar], startPoints[randomCarSpot].transform.position, startPoints[randomCarSpot].transform.rotation); 
            Instantiate(parkPrefabs[randomPark], parkPoints[randomParkSpot].transform.position, parkPoints[randomParkSpot].transform.rotation);
            Instantiate(powerUpPrefab, PowerUpSpawnPosition(), powerUpPrefab.transform.rotation);
            parkPoints.RemoveAt(randomParkSpot);
            carCount++;
            waveNumber++;
            CarController.FindObjectOfType<CarController>().isStun = true;
            cameraFollow.FindObjectOfType<cameraFollow>().targets.RemoveAt(0);
        }
        if (carCount == 9) // next level
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    private Vector3 PowerUpSpawnPosition()
    {
        float spawnPosAx = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosz = Random.Range(spawnRangeZmin, spawnRangeZmax);

        Vector3 powerUpSpawn = new Vector3(spawnPosAx, 0, spawnPosz);
        return powerUpSpawn;
    }
}
