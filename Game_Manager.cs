using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [SerializeField]
    private GlassFill Glass1;
    [SerializeField]
    private GlassFill Glass2;
    [SerializeField]
    private GlassFill Glass3;
    [SerializeField]
    private GlassFill Glass4;
    public int filled;
    private int Previousfilled;
    [HideInInspector]
    public int rnd = 0;
    private int count1;
    private int count2;
    private int count3;
    private int count4;
    private int Previousrnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        filled = 0;
        rnd = 0;
        count1 = Glass1.count;
        count2 = Glass2.count;
        count3 = Glass3.count;
        count4 = Glass4.count;
        filled = count1 + count2 + count3 + count4;
        if (filled > 3 && filled > Previousfilled && filled < 35)
        {
            rnd = Random.Range(1, 5);
            while (Previousrnd == rnd)
            {
                rnd = Random.Range(1, 5);
            }
            Previousfilled = filled;
            Previousrnd = rnd;
        } 
    }
}
