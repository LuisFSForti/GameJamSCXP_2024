using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvisoController : MonoBehaviour
{
    void Update()
    {
        if (Time.timeSinceLevelLoad >= 6f)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
