using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    public float Distance = 4f;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public float desiredDuration = 0.25f;
    private float elapsedTime = 0f;
    [SerializeField]
    private AnimationCurve curve;
    private float percentageComplete = 0f;
    private bool flagL = false;
    private bool flagR = false;
    private bool lockL;
    private bool lockR;
    public int NumberOfDispensers = 4;
    private int Number;
    Controller controls;
    private float push;
    private bool moveL;
    private bool moveR;
    public float threshold = 0.1f;
    public GameObject boneL;
    public GameObject boneR;
    public GameObject boneTipL;
    public GameObject boneTipR;
    public GameObject boneMiddleL;
    public GameObject boneMiddleR;
    public GameObject boneProxR;
    public GameObject Dispenser1;
    public GameObject Dispenser2;
    public GameObject Dispenser3;
    public GameObject Dispenser4;
    MeshRenderer mesh2;
    MeshRenderer mesh3;
    MeshRenderer mesh4;
    CapsuleCollider col2;
    CapsuleCollider col3;
    CapsuleCollider col4;
    private Quaternion currentRotation;
    private Vector3 currentEulerL;
    private Vector3 currentEulerR;
    private Vector3 currentEulerTipL;
    private Vector3 currentEulerTipR;
    private Vector3 currentEulerMiddleL;
    private Vector3 currentEulerMiddleR;
    private Vector3 currentEulerProxR;

    void Awake()
    {
        controls = new Controller();

        controls.Gameplay.Grasp.performed += ctx => push = ctx.ReadValue<float>();
        controls.Gameplay.Grasp.canceled += ctx => push = 0f;

        controls.Gameplay.Left.performed += ctx => moveL = true;
        controls.Gameplay.Left.canceled += ctx => moveL = false;

        controls.Gameplay.Right.performed += ctx => moveR = true;
        controls.Gameplay.Right.canceled += ctx => moveR = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        mesh2 = Dispenser2.GetComponent<MeshRenderer>();
        mesh3 = Dispenser3.GetComponent<MeshRenderer>();
        mesh4 = Dispenser4.GetComponent<MeshRenderer>();
        col2 = Dispenser2.GetComponent<CapsuleCollider>();
        col3 = Dispenser3.GetComponent<CapsuleCollider>();
        col4 = Dispenser4.GetComponent<CapsuleCollider>();
        if (NumberOfDispensers == 3)
        {   
            mesh4.enabled = false;
            col4.enabled = false;
            Dispenser2.transform.position = new Vector3(0f, 3.65f, 0f);
            Dispenser1.transform.position = new Vector3(Distance, 3.65f, 0f);
            Dispenser3.transform.position = new Vector3(-Distance, 3.65f, 0f);
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }
        if (NumberOfDispensers == 2)
        {   
            mesh4.enabled = false;
            mesh3.enabled = false;
            col4.enabled = false;
            col3.enabled = false;
            Dispenser1.transform.position = new Vector3(Distance/2f, 3.65f, 0f);
            Dispenser2.transform.position = new Vector3(-Distance/2f, 3.65f, 0);
        }
        if (NumberOfDispensers == 1)
        {
            mesh4.enabled = false;
            mesh3.enabled = false;
            mesh2.enabled = false;
            col4.enabled = false;
            col3.enabled = false;
            col2.enabled = false;
            Dispenser1.transform.position = new Vector3(0f, 3.65f, 0f);
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (percentageComplete > 1)
        {
            flagL = false;
            flagR = false;
        }
        if (moveL == true && flagL == false && flagR == false && lockL == false)
        {
            flagL = true;
            flagL = true;
            elapsedTime = 0;
            percentageComplete = 0;
            startPosition = transform.position;
            endPosition = new Vector3(-Distance + transform.position.x, transform.position.y, transform.position.z);
        }

        if (percentageComplete < 1 && flagL == true)
        {
            percentageComplete = elapsedTime / desiredDuration;
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percentageComplete));
        }

        if (moveR == true && flagR == false && flagL == false && lockR == false)
        {
            flagR = true;
            elapsedTime = 0;
            percentageComplete = 0;
            startPosition = transform.position;
            endPosition = new Vector3(Distance + transform.position.x, transform.position.y, transform.position.z);
        }

        if (percentageComplete < 1 && flagR == true)
        {
            elapsedTime += Time.deltaTime;
            percentageComplete = elapsedTime / desiredDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percentageComplete));
        }
        if (push > threshold)
        {
            float angleL = 90f + (95f-90f)*push;
            currentEulerL = new Vector3(angleL, 180f, 180f);
            boneL.transform.localEulerAngles = currentEulerL;
            float angleR = -20f + (-9f+20f)*push;
            currentEulerR = new Vector3(angleR, 0f, 0f);
            boneR.transform.localEulerAngles = currentEulerR;
            float angleTipL = -30f + (-60f+30f)*push;
            currentEulerTipL = new Vector3(angleTipL, 0f, 0f);
            boneTipL.transform.localEulerAngles = currentEulerTipL;
            float angleTipR = 0f + (80f-0f)*push;
            currentEulerTipR = new Vector3(angleTipR, 0f, 0f);
            boneTipR.transform.localEulerAngles = currentEulerTipR;
            float angleMiddleL = -10f + (-15f+10f)*push;
            currentEulerMiddleL = new Vector3(angleMiddleL, 0f, 0f);
            boneMiddleL.transform.localEulerAngles = currentEulerMiddleL;
            float angleMiddleR = 20f + (50f-20f)*push;
            currentEulerMiddleR = new Vector3(angleMiddleR, 0f, 0f);
            boneMiddleR.transform.localEulerAngles = currentEulerMiddleR;
            float angleProxR = 36f + (50f-36f)*push;
            currentEulerProxR = new Vector3(angleProxR, 0f, 0f);
            boneProxR.transform.localEulerAngles = currentEulerProxR;
        }
        if (push <= threshold)
        {
            currentEulerL = new Vector3(90, 180f, 180f);
            boneL.transform.localEulerAngles = currentEulerL;
            currentEulerR = new Vector3(-20, 0f, 0f);
            boneR.transform.localEulerAngles = currentEulerR;
            currentEulerTipL = new Vector3(-30f, 0f, 0f);
            boneTipL.transform.localEulerAngles = currentEulerTipL;
            currentEulerTipR = new Vector3(0f, 0f, 0f);
            boneTipR.transform.localEulerAngles = currentEulerTipR;
            currentEulerMiddleL = new Vector3(-10f, 0f, 0f);
            boneMiddleL.transform.localEulerAngles = currentEulerMiddleL;
            currentEulerMiddleR = new Vector3(20f, 0f, 0f);
            boneMiddleR.transform.localEulerAngles = currentEulerMiddleR;
            currentEulerProxR = new Vector3(36f, 0f, 0f);
            boneProxR.transform.localEulerAngles = currentEulerProxR;
        }
        //Debug.Log(boneL.transform.localEulerAngles.x);
    }
    
    void OnCollisionEnter(Collision collisionInfo)
    {
        int.TryParse(collisionInfo.collider.tag, out Number);
        if (NumberOfDispensers == 1)
        {
            lockL = true;
            lockR = true;
        }
        else if (Number == NumberOfDispensers)
        {
            lockL = true;
            lockR = false;
        }

        else if (Number == 1)
        {
            lockL = false;
            lockR = true;
        }
        else
        {
            lockL = false;
            lockR = false;
        }
        Debug.Log(Number);
    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
}
