using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaScia : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem sciaCorsa = new ParticleSystem();
    public ParticleSystem sciaSalto = new ParticleSystem();

    public GameObject generatoreDiScia = new GameObject();
    private StarterAssets.ThirdPersonController playerController;
    

    void Start()
    {
        playerController = generatoreDiScia.GetComponent<StarterAssets.ThirdPersonController>();
        sciaSalto = this.sciaSalto;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
    }

    private void SciaSalto()
    {
        
        if (staSaltando())
        {
            sciaSalto.gameObject.SetActive(true);
            sciaCorsa.Stop();
           

        }
    }

    bool staSaltando()
    {
        
        Debug.Log(generatoreDiScia.name 
            + "salta "+ playerController.Grounded!);
        return playerController.Grounded!;

    }
    bool staCorrendo()
    {
        return playerController.Moving;
    }
}
