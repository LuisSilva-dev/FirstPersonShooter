using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private KeyCode shoot = KeyCode.Mouse0;
    public GameObject granade;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shoot))
        {
            Instantiate(granade, transform.position + transform.up * -1.2f + transform.forward * 0.5f + transform.right * -0.3f, transform.rotation * Quaternion.Euler(90, 0, 0)).AddComponent<BulletGranade>();
        }
    }
}
