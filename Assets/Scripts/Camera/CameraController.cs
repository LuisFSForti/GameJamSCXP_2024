using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Camera Cam;

    // Update is called once per frame
    void Update()
    {
        if(Player != null)
            Cam.orthographicSize = 10 * Player.transform.localScale.x;

        if (Cam.orthographicSize > 400)
            Cam.orthographicSize = 400;
    }

    private void OnEnable()
    {
        Cam.orthographicSize = 10 * Player.transform.localScale.x;
        transform.position = Player.transform.position + new Vector3(0,0,-10);
    }
}
