using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int Height;

    private Camera cam;
    private GameObject player;

    private void Start() 
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate() 
    {
        cam.transform.position = player.transform.position + new Vector3(0,Height,0); 
    }
}
