using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

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
    [HideInInspector]
    public bool pushLock;
    public int NumberOfDispensers = 4;
    [HideInInspector]
    public int Number;
    //Controller controls;
    private float push;
    private bool moveL;
    private bool moveR;
    public float threshold = 0.05f;
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
    private Vector3 currentEulerL;
    private Vector3 currentEulerR;
    private Vector3 currentEulerTipL;
    private Vector3 currentEulerTipR;
    private Vector3 currentEulerMiddleL;
    private Vector3 currentEulerMiddleR;
    private Vector3 currentEulerProxR;
    public float scale = 1f;
    public GameObject SqueezeSystem1;
    public GameObject SqueezeSystem2;
    public GameObject SqueezeSystem3;
    public GameObject SqueezeSystem4;
    [HideInInspector]
    public float posz;
    [SerializeField]
    private SampleUserPolling_ReadWrite PushParameter;
    public float MoveForward = 10;
    
    void Start()
    {
        if (NumberOfDispensers == 3)
        {   
            Dispenser4.SetActive(false);
            SqueezeSystem4.SetActive(false);
            Dispenser2.transform.position = new Vector3(0f, 4f, 0f);
            SqueezeSystem2.transform.position = new Vector3(0f, 4f, 0f);
            Dispenser1.transform.position = new Vector3(Distance, 4f, 0f);
            SqueezeSystem1.transform.position = new Vector3(Distance, 4f, 0f);
            Dispenser3.transform.position = new Vector3(-Distance, 4f, 0f);
            SqueezeSystem3.transform.position = new Vector3(-Distance, 4f, 0f);
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }
        if (NumberOfDispensers == 2)
        {   
            Dispenser4.SetActive(false);
            SqueezeSystem4.SetActive(false);
            Dispenser3.SetActive(false);
            SqueezeSystem3.SetActive(false);
            Dispenser1.transform.position = new Vector3(Distance/2f, 4f, 0f);
            SqueezeSystem1.transform.position = new Vector3(Distance/2, 4f, 0f);
            Dispenser2.transform.position = new Vector3(-Distance/2f, 4f, 0);
            SqueezeSystem2.transform.position = new Vector3(-Distance/2, 4f, 0f);
        }
        if (NumberOfDispensers == 1)
        {
            Dispenser4.SetActive(false);
            SqueezeSystem4.SetActive(false);
            Dispenser3.SetActive(false);
            SqueezeSystem3.SetActive(false);
            Dispenser2.SetActive(false);
            SqueezeSystem2.SetActive(false);
            Dispenser1.transform.position = new Vector3(0f, 4f, 0f);
            SqueezeSystem1.transform.position = new Vector3(0f, 4f, 0f);
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }

    }

    // Update is called once per frame
    void Update()
    {
        push = PushParameter.push;
        moveL = Input.GetKeyDown(KeyCode.LeftArrow);
        moveR = Input.GetKeyDown(KeyCode.RightArrow);

        if (percentageComplete > 1)
        {
            flagL = false;
            flagR = false;
        }
        if (moveL == true && flagL == false && flagR == false && lockL == false && pushLock == false)
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

        if (moveR == true && flagR == false && flagL == false && lockR == false && pushLock == false)
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
        if (push > threshold && flagL == false && flagR == false)
        {
            pushLock = true;
            posz = -2.3f + (-0.3f + 2.3f)*(push-threshold)*scale*MoveForward;
            if (posz > -0.3f)
            {
                posz = -0.3f;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, posz);
            float angleL = 90f + (95f-90f)*push*scale;
            if (angleL > 95f)
            {
                angleL = 95f;
            }
            currentEulerL = new Vector3(angleL, 180f, 180f);
            boneL.transform.localEulerAngles = currentEulerL;
            float angleR = -20f + (-9f+20f)*push*scale;
            if (angleR > -9f)
            {
                angleR = -9f;
            }
            currentEulerR = new Vector3(angleR, 0f, 0f);
            boneR.transform.localEulerAngles = currentEulerR;
            float angleTipL = -30f + (-60f+30f)*push*scale;
            if (angleTipL < -60f)
            {
                angleTipL = -60f;
            }
            currentEulerTipL = new Vector3(angleTipL, 0f, 0f);
            boneTipL.transform.localEulerAngles = currentEulerTipL;
            float angleTipR = 0f + (80f-0f)*push*scale;
            if (angleTipR > 80f)
            {
                angleTipR = 80f;
            }
            currentEulerTipR = new Vector3(angleTipR, 0f, 0f);
            boneTipR.transform.localEulerAngles = currentEulerTipR;
            float angleMiddleL = -10f + (-15f+10f)*push*scale;            
            if (angleMiddleL < -15f)
            {
                angleMiddleL = -15f;
            }
            currentEulerMiddleL = new Vector3(angleMiddleL, 0f, 0f);
            boneMiddleL.transform.localEulerAngles = currentEulerMiddleL;
            float angleMiddleR = 20f + (50f-20f)*push*scale;
            if (angleMiddleR > 50f)
            {
                angleMiddleR = 50f;
            }
            currentEulerMiddleR = new Vector3(angleMiddleR, 0f, 0f);
            boneMiddleR.transform.localEulerAngles = currentEulerMiddleR;
            float angleProxR = 36f + (50f-36f)*push*scale;
            if (angleProxR > 50f)
            {
                angleProxR = 50f;
            }
            currentEulerProxR = new Vector3(angleProxR, 0f, 0f);
            boneProxR.transform.localEulerAngles = currentEulerProxR;
        }
        if (push <= threshold)
        {
            pushLock = false;
            posz = -2.3f;
            transform.position = new Vector3(transform.position.x, transform.position.y, -2.3f);
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
    }
}
