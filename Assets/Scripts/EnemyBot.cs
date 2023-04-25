using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class EnemyBot : MonoBehaviour
{
    private NavMeshAgent agent;
    Animator anim;
    private Transform target;
    private float dist;
    [SerializeField] private Canvas EndGameMenu;

    void Start()
    {
        EndGameMenu.enabled = false;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<FirstPersonMovement>().transform;
    }


    void Update()
    {
        dist = Vector3.Distance(agent.transform.position, target.position);
        if (dist <= 2f)
        {
            EndGameMenu.enabled = true;
            Time.timeScale = 0f;
            SceneManager.LoadScene("Menu");
            return;
        }
        anim.SetFloat("speed", agent.speed);
        agent.SetDestination(target.position);
        
    }
}
