using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDestruction : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Droplet")
        {
            Destroy(col.gameObject);
            
        }
    }
}
