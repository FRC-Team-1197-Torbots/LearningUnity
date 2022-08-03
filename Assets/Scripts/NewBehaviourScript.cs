using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Encoder = new Encoder(0,1,2);
    int EValue = 0;
    public void resetEncoder(){
        Encoder.reset();
    }

    
    void Start()
    {
        Encoder.reset()

    }

    // Update is called once per frame
    void Update()
    {
        Evalue = int Encoder.get();
    }
}