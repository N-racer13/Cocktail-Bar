using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ValveSystem : MonoBehaviour
{
    Controller controls;
    private float push;
    private float scale;
    private float threshold;
    private bool hand = false;
    private float Scalefactor;
    [SerializeField]
    private Player_Movement HandParameters;
    private int tag;
    private int Number;
    private bool pushLock = false;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new Controller();

        controls.Gameplay.Grasp.performed += ctx => push = ctx.ReadValue<float>();
        controls.Gameplay.Grasp.canceled += ctx => push = 0f;
    }
    void Start()
    {
        scale = HandParameters.scale;
        threshold = HandParameters.threshold;
        int.TryParse(gameObject.tag, out tag);
    }

    // Update is called once per frame
    void Update()
    {
        Number = HandParameters.Number;
        pushLock = HandParameters.pushLock;
        if (tag == Number && push > threshold && pushLock == true)
        {
            //CurrentScale= transform.localScale
            Scalefactor = 1f + (0.65f-1f)*(push-threshold)*scale;
            if (Scalefactor < 0.65f)
            {
                Scalefactor = 0.65f;
            }
            transform.localScale = new Vector3(Scalefactor, transform.localScale.y, Scalefactor);
            
        }
        if (push <= threshold)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, 1f);
        }
    }
        void OnEnable()
    {
        controls.Gameplay.Enable();
    }
        public void Disable()
    {
        controls.Gameplay.Enable();
    }
}
