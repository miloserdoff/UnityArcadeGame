using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [SerializeField] private Text ScoreBoard;
    public Transform[] Points;
    public float Speed = 0.0f, Distance = 0.0f;
    public int _currentPoint;
    public int coins;
    private void Start()
    {
        _currentPoint = Random.Range(0, 13);
    }
    private void Update()
    {
        if (_currentPoint == Points.Length) _currentPoint = 0;
        float _currentDistance = Vector3.Distance(transform.position, Points[_currentPoint].position);
        if (_currentDistance <= Distance) _currentPoint++;
        transform.LookAt(Points[_currentPoint].position);
        transform.position = Vector3.MoveTowards(transform.position, Points[_currentPoint].position, Speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Money")
        {
            coins += 1;
            other.gameObject.SetActive(false);
            ScoreBoard.text = "Бот: " + coins.ToString();
        }
    }
}