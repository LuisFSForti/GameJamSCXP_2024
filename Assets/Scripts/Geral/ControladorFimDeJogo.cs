using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorFimDeJogo : MonoBehaviour
{
    private int Vitorias;

    private void Start()
    {
        Vitorias = 0;
    }

    public void Venceu()
    {
        Vitorias++;
        if(Vitorias >= 2) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(this.gameObject);
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Perdeu()
    {
        Vitorias = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int QuantasVitorias()
    {
        return Vitorias;
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 0)
            Destroy(this.gameObject);
    }
}
