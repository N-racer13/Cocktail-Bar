using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private Game_Manager Counter;
    public Text ScoreText;
    private int GlassesFilled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GlassesFilled = Counter.filled-5;
        ScoreText.text = "Glasses filled: "+ GlassesFilled.ToString();
    }
}
