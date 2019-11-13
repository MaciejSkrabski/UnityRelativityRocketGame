using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public float cameraDistX;
    public float cameraDistY;
    public float cameraDistZ;
    
    private Camera mainCamera;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerInfo = player.transform.transform.position;
        mainCamera.transform.position = new Vector3(playerInfo.x + cameraDistX, playerInfo.y + cameraDistY, playerInfo.z + cameraDistZ);
    }
}
