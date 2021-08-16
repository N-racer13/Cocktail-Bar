using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class ValveSystem : MonoBehaviour
{
    //Controller controls;
    private float push;
    private float scale;
    private float threshold;
    private float posz;
    private float Squeezefactor;
    [SerializeField]
    private Player_Movement HandParameters;
    private int tag;
    private int Number;
    public GameObject Fluid;
    private float Fluidfactor = 1f;
    public float FluidSensitivity = 0.001f;
    public Rigidbody FluidDrop;
    public float DropChance;
    private float SpawnChance;
    public Transform Spawnpoint;
    private Rigidbody RigidDrop;
    private Rigidbody RigidDrop2;
    [SerializeField]
    private AnimationCurve DropWidth;
    [SerializeField]
    private AnimationCurve SputterChance;
    public float Velocity;
    public float DropX;
    public float DropY;
    public float DropZ;
    public bool ExtraDrops;
    [HideInInspector]
    public float DropScale;
    [SerializeField]
    private SampleUserPolling_ReadWrite PushParameter;

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
        posz = HandParameters.posz;
        push = PushParameter.push;
        DropScale = DropWidth.Evaluate(push);
        if (tag == Number && posz == -0.3f)
        {
            SpawnChance = Random.Range(0f, 1f)*DropScale;
            Squeezefactor = 1f + (0.65f-1f)*(push-threshold)*scale;
            Fluidfactor = Fluidfactor - (DropScale-threshold)*scale*FluidSensitivity;
            if (Squeezefactor < 0.65f)
            {
                Squeezefactor = 0.65f;
            }
            if (Fluidfactor < 0f)
            {
                Fluidfactor = 0f;
                SpawnChance = 0;
            }
            transform.localScale = new Vector3(Squeezefactor, transform.localScale.y, Squeezefactor);
            Fluid.transform.localScale = new Vector3(Fluid.transform.localScale.x, Fluidfactor, Fluid.transform.localScale.z);
            if (SpawnChance > 1-DropChance)
            {
                Rigidbody RigidDrop;
                RigidDrop = Instantiate(FluidDrop, Spawnpoint.position, Spawnpoint.rotation) as Rigidbody;
                RigidDrop.transform.localScale = new Vector3(DropX*DropScale*scale, DropY, DropZ*DropWidth.Evaluate(push)*scale);
                RigidDrop.velocity = new Vector3(SputterChance.Evaluate(push)*10*Random.Range(-1f, 1f), -Velocity, SputterChance.Evaluate(push)*10*Random.Range(-1f, 1f));
                if (ExtraDrops == true)
                {
                    Rigidbody RigidDrop2;
                    RigidDrop2 = Instantiate(FluidDrop, Spawnpoint.position, Spawnpoint.rotation) as Rigidbody;
                    RigidDrop2.transform.localScale = new Vector3(DropX*DropScale*scale, DropY, DropZ*DropScale*scale);
                    RigidDrop2.velocity = new Vector3(SputterChance.Evaluate(push)*10*Random.Range(-1f, 1f), -Velocity, SputterChance.Evaluate(push)*10*Random.Range(-1f, 1f));
                }
            }
        }
        else
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, 1f);
        }
    }

}
