using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f,5f,-8f);

    void Start()
    {
        // Cursor.lockState = CursorLockMode.Confined;
    }

    void LateUpdate()
    {
        Vector3 offsetTarget = new Vector3(offset.x, player.transform.position.y, player.transform.position.z);
        transform.position = offsetTarget + offset;
    }
}
