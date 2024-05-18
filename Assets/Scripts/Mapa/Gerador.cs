using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerador : MonoBehaviour
{
    [SerializeField] private GameObject PrefabEstrela;
    [SerializeField] private float Raio, MassaAtual, Proporcao;
    public float MassaInicial;

    // Start is called before the first frame update
    void Start()
    {
        MassaAtual = MassaInicial * Proporcao;

        while (MassaAtual > 1)
        {
            GameObject estrela = Instantiate(PrefabEstrela, transform);
            float ang = Random.Range(0f, Mathf.PI * 2);
            estrela.transform.localPosition = new Vector2(Raio * Mathf.Cos(ang) * Random.Range(0.1f, 1f), Raio * Mathf.Sin(ang) * Random.Range(0.1f, 1f));

            float massa = 0;

            do
            {
                massa = Random.Range(0.0001f, 1.0f) * Proporcao;
            } while (massa > MassaAtual);

            estrela.GetComponent<Rigidbody2D>().mass = massa;
            MassaAtual -= massa;

            estrela.GetComponent<MecanicaPrincipal>().Calor = Random.Range(1f, 3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
