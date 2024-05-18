using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MorteController : MonoBehaviour
{
    [SerializeField] private float DuracaoTransicao, TempoInicial;
    [SerializeField] Material Tela;

    // Update is called once per frame
    void Update()
    {
        Tela.color += new Color(0, 0, 0, 1f / DuracaoTransicao * Time.deltaTime);


        if (Time.time >= TempoInicial + DuracaoTransicao)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorFimDeJogo>().Perdeu();
        }
    }

    private void OnEnable()
    {
        TempoInicial = Time.time;
    }

    private void OnDisable()
    {
        Tela.color = new Color(0,0,0,0);
    }
}
