using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassFill : MonoBehaviour
{
    public float FillStart;
    public float FillEnd;
    private Renderer rend;
    [SerializeField]
    private ValveSystem FluidParameters;
    private float DropScale;
    public float FillSpeed;
    public GameObject Glass;
    MeshRenderer GlassMesh;
    public GameObject Full;
    MeshRenderer FluidMesh;
    MeshCollider FluidCollider;
    private float Spawn;
    public float SpawnChance;
    private bool GlassPresent = true;
    private float counter;
    private int glassnumber;
    private int randomnumber;
    [HideInInspector]
    public int count = 0;
    [SerializeField]
    private Game_Manager RandomNumber;

    // Start is called before the first frame update
    void Start()
    {
        int.TryParse(gameObject.tag, out glassnumber);
        rend = gameObject.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Shader Graphs/FluidFiller");
        counter = FillStart;
        GlassMesh = Glass.GetComponent<MeshRenderer>();
        FluidMesh = gameObject.GetComponent<MeshRenderer>();
        FluidCollider = gameObject.GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        DropScale = FluidParameters.DropScale;
        if (rend.material.GetFloat("Vector1_3EA5C93") > FillEnd)
        {
            count += 1;
            Full.SetActive(true);
            GlassMesh.enabled = false;
            FluidMesh.enabled = false;
            FluidCollider.enabled = false;
            rend.material.SetFloat("Vector1_3EA5C93", FillStart);
            GlassPresent = false;
        }
        randomnumber = RandomNumber.rnd;
        if (randomnumber == glassnumber && GlassPresent == false)
        {
            counter = FillStart;
            Full.SetActive(false);
            GlassMesh.enabled = true;
            FluidMesh.enabled = true;
            FluidCollider.enabled = true;
            GlassPresent = true;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Droplet")
        {
            Destroy(col.gameObject);
            counter += FillSpeed*DropScale;
            rend.material.SetFloat("Vector1_3EA5C93", counter);
        }
    }
}
