using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LanterMan : MonoBehaviour
{
    public float sightRange;
    private Animator anim;
    private GameObject player;
    private float seconds;
    public int attackDelaySec;
    public float perfectDist;
    private bool saw;
    public float walkSpeed;
    public float walkTime;
    private float timer;
    private int typeOfDirection;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        seconds = 0;
        timer = 0;
        typeOfDirection = 0;
        saw = false;
        player = GameObject.Find("Player Body");
        anim = gameObject.GetComponent<Animator>();
        GameObject child = gameObject.transform.GetChild(1).gameObject;
        MeshCollider collider = child.AddComponent<MeshCollider>();
        collider.sharedMesh = child.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        collider.convex = true;
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Assets/LanternManMoving", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
        anim.SetBool("isAttacking", false);
        anim.SetBool("isMoving", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            GameObject lanternLight = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            lanternLight.transform.position = transform.position - transform.forward * 0.5f + transform.up * 0.4f;
            lanternLight.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isMoving", true);
        }
        else if ((Physics.CheckSphere(transform.position, sightRange, LayerMask.GetMask("PlayerLayer")) || saw) && seconds == 0f)
        {
            saw = true;
            AttackPlayer();
        }
        else
        {
            Move();
        }
        seconds += 1/60f;
        if (seconds >= attackDelaySec)
        {
            Debug.Log("im here");
            seconds = 0f;
        }
    }

    private void AttackPlayer()
    {
        Vector3 lookPos = transform.position - player.transform.position;
        Vector3 vec = new Vector3(lookPos.x, lookPos.y, lookPos.z);
        vec.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vec), Time.smoothDeltaTime * 200);
        anim.SetBool("isAttacking", true);
        anim.SetBool("isMoving", false);
    }

    private void Move()
    {
        Vector3 vec = transform.position - player.transform.position;
        if((timer > 0 && typeOfDirection == 0) || vec.magnitude < perfectDist)
        {
            timer++;
            typeOfDirection = 0;

            transform.position = transform.position + transform.forward * walkSpeed;
            if(timer >= 60*walkTime)
            {
                timer = 0;
            }
        }
        else if((timer > 0 && typeOfDirection == 2)  || vec.magnitude > perfectDist)
        {
            timer++;
            typeOfDirection = 2;
            transform.position = transform.position - transform.forward * walkSpeed;
            if(timer >= 60*walkTime)
            {
                timer = 0;
            }
        }
    }
}
