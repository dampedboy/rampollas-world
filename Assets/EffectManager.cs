using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem sciaCorsa ;
    public ParticleSystem sciaSalto ;

    public bool salta;
    public bool corre;

    public GameObject generatoreDiScia;
    private PlayerMovement playerController;
    

    void Start()
    {
        playerController = generatoreDiScia.GetComponent<PlayerMovement>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        corre = staCorrendo();
        salta = staSaltando();
        SciaSalto();
        SciaCorsa();

    }

    private void SciaCorsa()
    {
        sciaCorsa.gameObject.SetActive(true);
        ParticleSystem psCorsa = sciaCorsa.GetComponent<ParticleSystem>();
        var module = psCorsa.emission;
        if (playerController.isGrounded)
        {
            module.rateOverTime = (staCorrendo()) ? 10.0f : 0.0f;
        }
        else
        {
            module.rateOverTime = 0.0f;
        }
        
    }

    private void SciaSalto()
    {
        sciaSalto.gameObject.SetActive(staSaltando());        
    }

    public bool staSaltando()
    {
        return playerController.isJumping;
    }
    public bool staCorrendo()
    {
        return playerController.isMoving;
    }
}
