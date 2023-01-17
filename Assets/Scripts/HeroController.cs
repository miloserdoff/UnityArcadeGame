using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public float speed;

    private Vector3 moveVector;
    private CharacterController h_controller;
    private Animator anim;
    [SerializeField] private int coins;
    void Start()
    {
        h_controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HeroMove();
    }

    private void HeroMove()
    {
        moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal") * speed;
        moveVector.z = Input.GetAxis("Vertical") * speed;

        h_controller.Move(moveVector * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Money")
        {
            coins += 1;
            other.gameObject.SetActive(false);
        }
    }
}
