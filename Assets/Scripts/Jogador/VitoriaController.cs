using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VitoriaController : MonoBehaviour
{
    [SerializeField] Material Tela;
    [SerializeField] private MecanicaPrincipal ControladorMecanicaPrincipal;

    [SerializeField] private float TempoInicial, TempoDuracao, DuracaoTransicao;

    [SerializeField] AudioSource AudioDistor, AudioQuebrar;

    // Start is called before the first frame update
    void Start()
    {
        ControladorMecanicaPrincipal.Calor = 1f;
        TempoInicial = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time <= TempoInicial + TempoDuracao/3f) 
        {
            ControladorMecanicaPrincipal.Calor = Random.Range(1, 4);
        }
        else
        {
            ControladorMecanicaPrincipal.Calor = Random.Range(4, 23);   
        }

        ControladorMecanicaPrincipal.Crescer();

        if (Time.time > TempoInicial + TempoDuracao)
        {
            Tela.color += new Color(0, 0, 0, 1f / DuracaoTransicao * Time.deltaTime);
            AudioDistor.gameObject.SetActive(false);
            AudioQuebrar.gameObject.SetActive(true);
        }
        if (Time.time >= TempoInicial + DuracaoTransicao + TempoDuracao && !AudioQuebrar.isPlaying)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorFimDeJogo>().Venceu();
        }
    }

    private void OnEnable()
    {
        ControladorMecanicaPrincipal.Calor = 1f;
        TempoInicial = Time.time;
        AudioDistor.gameObject.SetActive(true);
        AudioDistor.Play();
    }

    private void OnDisable()
    {
        Tela.color = new Color(1, 1, 1, 0);
        AudioDistor.gameObject.SetActive(false);
    }
}
