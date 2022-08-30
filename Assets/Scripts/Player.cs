using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public bool MoveInPC;
    public float playerSpeedMouse = 25f;
    public float playerSpeedTouch = 0.25f;
    float mouseX;

    [SerializeField] FixedTouchField touchField;

    void Start()
    {
        
    }

    void Update()
    {
        if(!MoveInPC) TouchMove();
        else InputMove();

        float velocity = SongManager.velocity;
        Vector3 movement = new Vector3(mouseX, 0, velocity);
        transform.Translate(movement * Time.deltaTime);  
    }

    void LateUpdate()
    {
        Vector3 p = transform.position;
        if (p.x >= 4)
        {
            p.x = 4;
            transform.position = p;
        }
        else if(p.x <= -4)
        {
            p.x = -4;
            transform.position = p;
        }    
    }

    void InputMove()
    {
        if (Input.GetMouseButton(0))
        {
            mouseX = Input.GetAxis("Mouse X") * playerSpeedMouse;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseX = 0;
        }
    }

    void TouchMove()
    {
        mouseX = touchField.TouchDist.x * playerSpeedTouch;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            Time.timeScale = 0;
            SongManager.Instance.audioSource.Stop();
        }
    }
}
