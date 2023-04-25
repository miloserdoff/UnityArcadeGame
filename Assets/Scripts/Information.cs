using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Information : MonoBehaviour
{
    [SerializeField] private int coins;
    [SerializeField] public int counts;
    [SerializeField] private int frees;
    [SerializeField] private Text TextCoins;
    [SerializeField] private Text FreePersonsText;
    [SerializeField] private Canvas LevelInformationMenu;
    public static bool IsGameStarted = false;
    void Start()
    {
        FreePersonsText.text = $"0/{counts.ToString()}";
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = 1f;
            LevelInformationMenu.enabled = false;
            IsGameStarted = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Village")
        {
            coins += Random.Range(1, 5);
            frees += 1;
            TextCoins.text = coins.ToString();
            FreePersonsText.text = $"{frees.ToString()}/{counts.ToString()}";
            other.gameObject.SetActive(false);

            if (frees == counts)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}

