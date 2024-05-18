using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JogadorController : MonoBehaviour
{
    [SerializeField] Camera CameraMorte;

    [SerializeField] Rigidbody2D Corpo;
    [SerializeField] float Velocidade;

    [SerializeField] private float Tamanho, TamanhoMax, Crescimento, TempoEstado, CresimentoEstado, Objetivo;
    [SerializeField] private MecanicaPrincipal ControladorMecanicaPrincipal;

    private bool Ganhou, Vencendo;
    [SerializeField] VitoriaController ControladorVitoria;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Musica").GetComponent<AudioSource>().UnPause();
        CameraMorte.gameObject.SetActive(false);
        Tamanho = 1f;
        TempoEstado = 0f;
        ControladorMecanicaPrincipal.Calor = Tamanho;
        ControladorMecanicaPrincipal.Crescer();

        Objetivo = GameObject.Find("Mapa").GetComponent<Gerador>().MassaInicial * 9.5f / 10;

        Ganhou = false;
        Vencendo = false;
        ControladorVitoria.enabled = false;
    }

    private void Update()
    {
        if (!Ganhou)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }

            float variacao = Velocidade * Corpo.mass;
            Corpo.AddForce(new Vector2(variacao * Input.GetAxisRaw("Horizontal"), variacao * Input.GetAxisRaw("Vertical")));

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp(KeyCode.Space))
                TempoEstado = 0f;


            if (Input.GetKey(KeyCode.Space))
            {
                if (TempoEstado >= 1f)
                    TempoEstado = 1f;
                else
                    TempoEstado += CresimentoEstado * Time.deltaTime;
            }
            else
            {
                if (TempoEstado <= -1f)
                    TempoEstado = -1f;
                else
                    TempoEstado -= CresimentoEstado * Time.deltaTime;
            }

            if (TempoEstado > 0)
            {
                if (Tamanho >= TamanhoMax)
                    Tamanho = TamanhoMax;
                else
                    Tamanho += Crescimento * Time.deltaTime * TempoEstado;
            }
            else
            {
                if (Tamanho > 1f)
                    Tamanho += Crescimento / 2 * Time.deltaTime * TempoEstado;
                else
                    Tamanho = 1f;
            }

            ControladorMecanicaPrincipal.Calor = Tamanho;
            ControladorMecanicaPrincipal.Crescer();

            if (Corpo.mass >= Objetivo)
            {
                Ganhou = true;
                TempoEstado = 0f;
            }
        }
        else if(!Vencendo)
        {
            TempoEstado += CresimentoEstado * Time.deltaTime;
            Corpo.velocity = (Vector3.zero - transform.position) * Velocidade * TempoEstado;
            Tamanho -= Time.deltaTime;

            if (Tamanho <= 1f)
            {
                Tamanho = 1f;

                if((Vector3.zero - transform.position).magnitude < 1f)
                {
                    Vencendo = true;
                    ControladorVitoria.enabled = true;
                    GameObject.Find("Musica").GetComponent<AudioSource>().Pause();
                }
            }

            ControladorMecanicaPrincipal.Calor = Tamanho;
            ControladorMecanicaPrincipal.Crescer();
        }
    }

    private void OnDisable()
    {
        CameraMorte.gameObject.SetActive(true);
    }
}
