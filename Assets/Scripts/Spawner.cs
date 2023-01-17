using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemy;
    public Transform[] spawnPoint;
    private int rand;
    [SerializeField] public static int BotsCountAll = 7;
    private int randPosition;
    public float startTimeBtwSpawns;
    private float timeBtwSpawns;

    void Start()
    {
        for (int i = 1; i <= BotsCountAll; i++)
        {
            randPosition = Random.Range(0, spawnPoint.Length);
            Instantiate(enemy[0], spawnPoint[randPosition].transform.position, Quaternion.identity);
        }
        
        
    }
    /*
    void Update()
    {

        if (timeBtwSpawns <= 0)
        {
            rand = Random.Range(0, enemy.Length);
            randPosition = Random.Range(0, spawnPoint.Length);
            Instantiate(enemy[rand], spawnPoint[randPosition].transform.position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
    */
}
