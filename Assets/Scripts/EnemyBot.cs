using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class EnemyBot : MonoBehaviour
{
    private NavMeshAgent agent;
    Animator anim;
    private Transform target;
    private float dist;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<FirstPersonMovement>().transform;
    }


    void Update()
    {
        dist = Vector3.Distance(agent.transform.position, target.position);
        if (dist <= 2f)
        {
            SceneManager.LoadScene("Menu");
            return;
        }
        anim.SetFloat("speed", agent.speed);
        agent.SetDestination(target.position);
        
    }
}
