using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassFill : MonoBehaviour
{
    private float counter = 1.9f;
    private Renderer rend;
    [SerializeField]
    private ValveSystem FluidParameters;
    private float DropScale;
    public float FillSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Shader Graphs/FluidFiller");
    }

    // Update is called once per frame
    void Update()
    {
        DropScale = FluidParameters.DropScale;
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
