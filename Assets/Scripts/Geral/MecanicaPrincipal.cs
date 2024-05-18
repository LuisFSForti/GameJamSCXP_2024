using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MecanicaPrincipal : MonoBehaviour
{
    [SerializeField] private Material MaterialBase;
    [SerializeField] private SpriteRenderer CorCorpo;
    public float Calor;

    private Rigidbody2D Corpo;
    private const float ConstGrav = 6.67f;
    private List<string> Entidades = new List<string> { "Player", "Estrela" };

    // Start is called before the first frame update
    void Start()
    {
        Corpo = this.GetComponent<Rigidbody2D>();
        Crescer();
    }

    // Update is called once per frame
    void Update()
    {
        if(Corpo.mass > 0.5f)
        {
            float distanciaArbitraria = Mathf.Sqrt(Corpo.mass * 2 * ConstGrav / 0.01f);
            Collider2D[] proximo = Physics2D.OverlapCircleAll(transform.position, distanciaArbitraria);
            foreach (var corpo in proximo)
            {
                if (corpo.gameObject != this.gameObject && Entidades.Contains(corpo.gameObject.tag))
                {
                    Atrair(corpo.gameObject);
                }
            }
        }
    }

    public void Crescer()
    {
        float raio = Mathf.Sqrt(Corpo.mass) * Calor;
        this.transform.localScale = new Vector3(raio, raio, 1);

        if(Calor < 4)
        {
            float proporcao = 3 / 2f - Calor / 2f;

            Material cor = new Material(MaterialBase);
            cor.color = Color.red * proporcao + Color.cyan * (1 - proporcao);
            CorCorpo.material = cor;
        }
        else
        {
            float proporcao = (23 / 19f - Calor / 19f)/2;

            Material cor = new Material(MaterialBase);
            cor.color = Color.cyan * proporcao + Color.white * (1 - proporcao);
            CorCorpo.material = cor;
        }
    }

    void Atrair(GameObject alvo)
    {
        Vector2 direcao = this.transform.position - alvo.transform.position;
        float forca = Time.deltaTime * ConstGrav * alvo.GetComponent<Rigidbody2D>().mass * Corpo.mass / Mathf.Pow(direcao.magnitude, 2);

        alvo.GetComponent<MecanicaPrincipal>().Aproximar(direcao, forca);
    }

    void Aproximar(Vector2 direcao, float forca)
    {
        Corpo.AddForce(direcao * forca);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.enabled || !this.enabled)
            return;

        if (!Entidades.Contains(collision.gameObject.tag))
        {
            return;
        }

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
