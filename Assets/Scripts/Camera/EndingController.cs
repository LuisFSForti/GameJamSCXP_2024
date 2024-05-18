using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{
    [SerializeField] private Camera Cam;

    [SerializeField] GameObject Tela;
    private float UltimoSalto;

    // Start is called before the first frame update
    void Start()
    {
        Tela.SetActive(false);
        UltimoSalto = Time.timeSinceLevelLoad + 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Cam.orthographicSize += (10 + 0.1f * Cam.orthographicSize) * Time.deltaTime;

        if (Time.timeSinceLevelLoad >= UltimoSalto)
        {
            Cam.orthographicSize *= 1.3f;
            UltimoSalto += 2f;
        }
        if(Cam.orthographicSize >= 2000)
        {
            Tela.SetActive(true);
            StartCoroutine(Sleep(2));
        }
    }

    IEnumerator Sleep(float Tempo)
    {
        yield return new WaitForSeconds(Tempo);

        SceneManager.LoadScene(0);
    }
}
