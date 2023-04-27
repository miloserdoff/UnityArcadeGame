using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

public class SkillEvent : MonoBehaviour
{
    public Text informationText;
    public GameObject[] villages;
    public Text timerText;
    public Rigidbody EnemyBot;
    public Animator anim;
    public NavMeshAgent agent;
    public ParticleSystem FreezeAnimation;
    public bool IsEnemy = false;
    public float UnfreezeBotSeconds;
    public float CooldownFreezeSkill;
    public float ShowVisibleInformationTimer;
    public float CooldownVisibleTimer;
    private bool isRunning = false;


    private void Start()
    {
        if (!IsEnemy)
        {
            anim.enabled = false;
            agent.enabled = false;
        }

        FreezeAnimation.Stop();
        EnemyBot.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (IsEnemy)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (isRunning || !Information.IsGameStarted)
                {
                    //Debug.Log("Скилл в КД!");
                }

                else
                {
                    EnemyBot.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                    anim.speed = 0;
                    agent.speed = 0;
                    FreezeAnimation.Play();
                    
                    StartCoroutine(UnfreezeBotTimer(UnfreezeBotSeconds));
                    StartCoroutine(StartCooldownTimer(CooldownFreezeSkill));
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isRunning || !Information.IsGameStarted)
            {
                Debug.Log("Скилл в КД!");
            }
            
            else
            {
                informationText.text = $"До ближайшего жителя {FindClosestVillager()} м.";
                informationText.enabled = true;
                StartCoroutine(StartCooldownTimer(CooldownVisibleTimer));
                StartCoroutine(ShowInformationVisibleTimer(ShowVisibleInformationTimer));
            }
        }

    }

    private float FindClosestVillager()
    {
        villages = GameObject.FindGameObjectsWithTag("Village");

        var minimumDistance = Mathf.Infinity;

        foreach (var go in villages)
        {
            var distance = Vector3.Distance(transform.position, go.transform.position);
            
            if (distance < minimumDistance)
            {
                minimumDistance = distance;
            }
        }

        return minimumDistance;
    }

    private IEnumerator UnfreezeBotTimer(float tmax)
    {
        var passedTime = 0.0f;

        while (passedTime < tmax)
        {
            passedTime += Time.deltaTime;
            yield return null;
        }

        FreezeAnimation.Stop();
        EnemyBot.constraints = RigidbodyConstraints.None;
        anim.speed = 1;
        agent.speed = 2;
    }

    private IEnumerator ShowInformationVisibleTimer(float tmax)
    {
        var passedTime = 0.0f;

        while (passedTime < tmax)
        {
            informationText.text = $"До ближайшего жителя {FindClosestVillager()} м.";
            passedTime += Time.deltaTime;
            yield return null;
        }

        informationText.enabled = false;
    }

    public IEnumerator StartCooldownTimer(float tmax)
    {
        timerText.enabled = true;
        isRunning = true;
        var passedTime = 0.0f;

        while (passedTime < tmax)
        {
            string seconds = Mathf.RoundToInt(tmax - passedTime).ToString("f0");
            timerText.text = $"CD: {seconds}";
            passedTime += Time.deltaTime;
            yield return null;
        }

        timerText.enabled = false;
        isRunning = false;
    }
}
