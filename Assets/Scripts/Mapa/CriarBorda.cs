using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CriarBorda : MonoBehaviour
{
    //https://www.youtube.com/watch?v=HU8UZFaUImQ

    [SerializeField] private LineRenderer Linha;
    [SerializeField] private EdgeCollider2D Colisao;

    [SerializeField] private int Divisoes;
    [SerializeField] private float Raio;

    // Start is called before the first frame update
    void Start()
    {
        float passoAngulo = 2f * Mathf.PI / Divisoes;

        Linha.positionCount = Divisoes;
        List<Vector2> pontos = new List<Vector2>();

        for (int i = 0; i < Divisoes; i++)
        {
            float xPosition = Raio * Mathf.Cos(i * passoAngulo);
            float yPosition = Raio * Mathf.Sin(i * passoAngulo);

            Vector3 ponto = new Vector3(xPosition, yPosition, 0);
            Linha.SetPosition(i, ponto);

            xPosition = (Raio - 10) * Mathf.Cos(i * passoAngulo);
            yPosition = (Raio - 10) * Mathf.Sin(i * passoAngulo);

            ponto = new Vector3(xPosition, yPosition, 0);

            pontos.Add(ponto);
        }

        //Novamente o ponto inicial para fechar o círculo de colisões
        pontos.Add(pontos[0]);


        //https://www.reddit.com/r/Unity2D/comments/o9vzub/how_do_i_make_a_line_renderer_have_collision/
        Colisao.SetPoints(pontos);
    }
}
