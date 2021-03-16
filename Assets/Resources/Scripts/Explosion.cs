using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float alpha;
    private const float numFrames = 20;
    Color oldColor;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        alpha = 1;
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Animator animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load("Assets/ExplosionAnimation", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
        mat = Resources.Load("Materials/Explosion", typeof(Material)) as Material;
        oldColor = mat.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(alpha > 0)
        {
            Color initNewColor = new Color(oldColor.r, oldColor.g, oldColor.b, alpha);
            mat.SetColor("_Color", initNewColor);
            GetComponent<Renderer>().material = mat;
            alpha -= (1 / numFrames);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
