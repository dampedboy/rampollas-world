using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaScia : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem sciaCorsa;
    public GameObject sciaSalto;

    public GameObject generatoreDiScia;

    private Vector3 lastPos;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sciaSalto.SetActive(staSaltando());
        if (!staCorrendo())
        {
            sciaCorsa.Stop();
        }

    }

    bool staSaltando()
    {
        StarterAssets.ThirdPersonController playerController = generatoreDiScia.GetComponent<StarterAssets.ThirdPersonController>();
        Debug.Log(generatoreDiScia.name + "salta "+ playerController.Grounded!);
        return playerController.Grounded!;
        //return (Input.GetKeyDown(KeyCode.Space)) ? true : false;

    }
    bool staCorrendo()
    {
        return (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) ? true : false;
    }
}
