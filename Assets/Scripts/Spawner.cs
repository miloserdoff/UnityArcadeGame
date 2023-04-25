using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemy;
    public Transform[] spawnPoint;
    private int randPosition;
    public float startTimeBtwSpawns;

    [SerializeField] public static int botCount = 7;

    void Start()
    {
        for (int i = 1; i <= botCount; i++)
        {
            randPosition = Random.Range(0, spawnPoint.Length);
            Instantiate(enemy[0], spawnPoint[randPosition].transform.position, Quaternion.identity);
        }
    }
}
