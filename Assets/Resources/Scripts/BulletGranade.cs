using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGranade : MonoBehaviour
{
    private float gravity;
    private float vel;
    public const float VEL = 0.0005f;
    public const float G = -0.0001f;
    public const float rotationVel = -0.5f;
    // Start is called before the first frame update
    void Start()
    {
        vel = 0.2f;
        gravity = 0;
        gameObject.AddComponent<CapsuleCollider>().isTrigger = true;
        gameObject.AddComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/GranadeBullet", typeof(Material)) as Material;
        transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
    }

    // Update is called once per frame
    void Update()
    {
        gravity += G;
        vel -= VEL;
        if (Mathf.Round(transform.rotation.eulerAngles.x) != 90)
        {
            transform.position = transform.position + transform.forward * vel + transform.up * gravity * Time.smoothDeltaTime;
            transform.Rotate(Vector3.left * rotationVel);
        }
        else
        {
            transform.position = transform.position + transform.forward * -gravity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        Instantiate(Resources.Load("Assets/Explosion", typeof(GameObject)) as GameObject, position, rotation).AddComponent<Explosion>();
        Destroy(gameObject);
    }
}
