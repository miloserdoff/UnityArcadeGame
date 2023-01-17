using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Information : MonoBehaviour
{
    [SerializeField] private int coins;
    [SerializeField] public int counts = Spawner.BotsCountAll;
    [SerializeField] private int frees;
    [SerializeField] private Text TextCoins;
    [SerializeField] private Text FreePersonsText;
    [SerializeField] private Text FinalGame;
    void Start()
    {
        FreePersonsText.text = $"0/{counts.ToString()}";
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

