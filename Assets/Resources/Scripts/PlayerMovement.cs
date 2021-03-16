using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private CharacterController charaterController;
    public float jumpForce;
    public float grav;
    public bool isGrounded;
    public float sensitivity;
    private GameObject camera;
    private float rotationY;

    private KeyCode forward = KeyCode.W;
    private KeyCode backward = KeyCode.S;
    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;

    private KeyCode jump = KeyCode.Space;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Camera");
        Cursor.visible = false;
        charaterController = GetComponent<CharacterController>();
        Physics.gravity = new Vector3(0, grav, 0);
        isGrounded = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded)
        {
            isGrounded = false;
        }
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity * Time.smoothDeltaTime;
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivity * Time.smoothDeltaTime);
        float clamp = Mathf.Clamp(rotationY, -90f, 90f);
        if (clamp != -90f && clamp != 90f)
        {
            camera.transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * sensitivity * Time.smoothDeltaTime);
        }
        else
        {
            rotationY = clamp;
        }
        float forwardValue = 0;
        float rightValue = 0;
        if(Input.GetKey(forward))
        {
            forwardValue = 1;
        }
        if(Input.GetKey(backward))
        {
            forwardValue = -1;
        }
        if (Input.GetKey(left))
        {
            rightValue = -1;
        }
        if(Input.GetKey(right))
        {
            rightValue = 1;
        }
        Vector3 vec = transform.forward * forwardValue + transform.right * rightValue;
        vec.Normalize();
        charaterController.Move(vec * Time.smoothDeltaTime * speed);
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
}
