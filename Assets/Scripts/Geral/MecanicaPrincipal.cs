using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicaPrincipal : MonoBehaviour
{
    private Rigidbody2D Corpo;

    // Start is called before the first frame update
    void Start()
    {
        Corpo = this.GetComponent<Rigidbody2D>();
        Crescer();
    }

    // Update is called once per frame
    void Update()
    {
        float distanciaArbitraria = Mathf.Sqrt(Corpo.mass * 20 / 0.01f);
        Collider2D[] proximo = Physics2D.OverlapCircleAll(transform.position, distanciaArbitraria);
        foreach (var corpo in proximo)
        {
            if(corpo.gameObject != this.gameObject)
            {
                Atrair(corpo.gameObject);
            }
        }
    }

    void Crescer()
    {
        this.gameObject.transform.localScale = new Vector3(Corpo.mass, Corpo.mass, 1);
    }

    void Atrair(GameObject alvo)
    {
        Vector2 direcao = this.transform.position - alvo.transform.position;
        float forca = Time.deltaTime * 6.67f * alvo.GetComponent<Rigidbody2D>().mass * Corpo.mass / Mathf.Pow(direcao.magnitude, 2);

        alvo.GetComponent<MecanicaPrincipal>().Aproximar(direcao, forca);
    }

    void Aproximar(Vector2 direcao, float forca)
    {
        Corpo.AddForce(direcao * forca);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == null || this.gameObject == null)
            return;

        bool comeu = false;

        if (this.tag.CompareTo("Player") == 0)
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().mass <= Corpo.mass * 1.2f)
                comeu = true;
        }
        else if (collision.gameObject.tag.CompareTo("Player") == 0)
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().mass * 1.2f < Corpo.mass)
                comeu = true;
        }
        else if (collision.gameObject.GetComponent<Rigidbody2D>().mass <= Corpo.mass)
            comeu = true;

        if (comeu)
        {
            Corpo.mass += collision.gameObject.GetComponent<Rigidbody2D>().mass;
            Crescer();

            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
}
