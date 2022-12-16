using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bicycle : MonoBehaviour
{
    [HideInInspector]
    public Vector3 customerTargetPosicion;

    [HideInInspector]
    public bool getPizza;

    [SerializeField] private GameObject pizzaBox;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private ReloadMenu reloadMenu;

    private int score = 0;

    private void FixedUpdate()
    {
        if (transform.position.x < customerTargetPosicion.x && getPizza)
        {
            GivePizza();
        }
    }

    void GivePizza()
    {
        Instantiate(pizzaBox, customerTargetPosicion + new Vector3(-0.31f, 0.8f, 1.21f), Quaternion.Euler(0, 0, 0));

        score++;
        scoreText.text = score.ToString();

        customerTargetPosicion = Vector3.zero;
        getPizza = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            Death();
        }
    }

    void Death()
    {
        Camera.main.GetComponent<CameraController>().Fade(true);
        reloadMenu.OpenReloadMenu(score);

        scoreText.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void MoveForward()
    {
        transform.position += new Vector3(-0.04f, 0, 0);
    }
}
