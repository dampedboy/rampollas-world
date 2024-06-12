using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portale : MonoBehaviour
{
    public Collider oggettoDaTeletrasportare;
    public GameObject altroPortale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(oggettoDaTeletrasportare.ToString()+" è entrato");
    }

    void OnTriggerEnter(Collider other)
    {
        oggettoDaTeletrasportare = other;
    }
}
