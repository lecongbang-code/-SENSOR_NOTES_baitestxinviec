using UnityEngine;

public class Player : MonoBehaviour
{
    public bool MoveInPC = true;
    public float playerSpeedMouse = 25f;
    public float playerSpeedTouch = 0.25f;

    [SerializeField] FixedTouchField touchField;

    float mouseX = 0f;
    float velocity = 0f;

    void Update()
    {
        if(!MoveInPC) TouchMove();
        else InputMove();
        if(!GameControl.finishGame) PlayerMove();
    }

    void PlayerMove()
    {
        velocity = SongManager.velocityPlayerMove;
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
            GameControl.FinishGame();
        }
    }
}