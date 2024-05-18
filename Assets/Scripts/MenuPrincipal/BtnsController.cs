using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnsController : MonoBehaviour
{
    [SerializeField] private Canvas Menu, Controles;

    private void Start()
    {
        Menu.gameObject.SetActive(true);
        Controles.gameObject.SetActive(false);
    }

    public void Jogar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TrocarControles()
    {
        Menu.gameObject.SetActive(false);
        Controles.gameObject.SetActive(true);
    }

    public void TrocarMenu()
    {
        Menu.gameObject.SetActive(true);
        Controles.gameObject.SetActive(false);
    }
}
