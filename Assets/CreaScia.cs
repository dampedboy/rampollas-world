using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaScia : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem scia;
    public GameObject generatoreDiScia;
    private Vector3 lastPos;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (siMuove())
        {
            //scia.Play();
            Debug.Log(generatoreDiScia + " si sta muovendo");
        }
        else
        {
            scia.Stop();
        }
        lastPos = transform.position;

    }

    bool siMuove()
    {
        return (Input.GetKeyDown(KeyCode.E))?true:false;
        
    }
}
