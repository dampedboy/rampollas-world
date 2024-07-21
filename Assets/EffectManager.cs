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

    public GameObject generatoreDiScia = new GameObject();
    private StarterAssets.ThirdPersonController playerController;
    

    void Start()
    {
        playerController = generatoreDiScia.GetComponent<StarterAssets.ThirdPersonController>();
        
        
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
        ParticleSystem ps = sciaCorsa.GetComponent<ParticleSystem>();
        var module = ps.emission;
        if (!staCorrendo())
        {
            module.rateOverTime = 10.0f;
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
        return !playerController.Grounded;
    }
    public bool staCorrendo()
    {
        return playerController.Moving;
    }
}
