using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    MapManager mapManager;
    public float cameraSpeed = 5f;
    public float zoomSpeed = 5f;
    int maxDistance;
	// Use this for initialization
	void Start () {
        mapManager = FindObjectOfType<MapManager>();
        maxDistance = mapManager.max(mapManager.m, mapManager.n);
        cameraSpeed = 20f;
        zoomSpeed = 20f;
}
	
	// Update is called once per frame
	void Update () {

        //Camera Speed Adjust:
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            if (cameraSpeed > 1)
                cameraSpeed -= 1;
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            if (cameraSpeed < 30)
                cameraSpeed += 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (zoomSpeed > 9)
                zoomSpeed -= 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (zoomSpeed < 30)
                zoomSpeed += 1;
        }

        //Camera Zoom Input:
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            GetComponent<Camera>().orthographicSize -= zoomSpeed * Time.deltaTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            GetComponent<Camera>().orthographicSize += zoomSpeed * Time.deltaTime;
        }
        //Vertical and Horizontal Movement Input:
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        
        float xLimit = transform.position.x + x * cameraSpeed * Time.deltaTime;
        float yLimit = transform.position.y + y * cameraSpeed * Time.deltaTime;
        if (xLimit <= -maxDistance || xLimit > maxDistance || yLimit <= -maxDistance || yLimit > maxDistance)
            return;
        transform.position += new Vector3(x, y, 0) * cameraSpeed * Time.deltaTime;

    }



}
