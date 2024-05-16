using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerador : MonoBehaviour
{
    [SerializeField] private GameObject PrefabEstrela;
    [SerializeField] private float LimHorizontal, LimVertical, Quantidade;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Quantidade;)
        {
            GameObject estrela = Instantiate(PrefabEstrela, transform);
            estrela.transform.position = new Vector2(Random.Range(-0.9f, 0.9f) * LimHorizontal, Random.Range(-0.9f, 0.9f) * LimVertical);
            estrela.GetComponent<Rigidbody2D>().mass = Random.Range(0.0001f, 0.1f);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
