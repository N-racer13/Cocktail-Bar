using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveSystem : MonoBehaviour
{
    Controller controls;
    private float pusht;
    public float scale = 1f;
    private bool hand = false;
    private float Scalefactor;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new Controller();

        controls.Gameplay.Grasp.performed += ctx => pusht = ctx.ReadValue<float>();
        controls.Gameplay.Grasp.canceled += ctx => pusht = 0f;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hand == true)
        {
            Scalefactor = 1f - (1f+0.65f)*pusht*scale;
            transform.localScale = new Vector3(transform.localScale.x*Scalefactor, transform.localScale.y, transform.localScale.z*Scalefactor);
            
        }
        Debug.Log(pusht);
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name == "Hand")
        {
            hand = true;
        }
        else
        {
            hand = false;
        }
    }
}
