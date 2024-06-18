using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portale : MonoBehaviour
{
    //public Collider oggettoDaTeletrasportare;
    public GameObject altroPortale;
    [SerializeField] Transform altroPortaleTransform;
    [SerializeField] Transform daTeletrasportare;
    private Transform distanza;

    // Start is called before the first frame update
    void Start()
    {
        altroPortaleTransform = altroPortale.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody != null)
        {
            daTeletrasportare = other.attachedRigidbody.GetComponent<Transform>();
            daTeletrasportare.position = altroPortaleTransform.position;

        }
        else if(other != null)
        {
            daTeletrasportare = other.GetComponentInParent<Transform>();
            daTeletrasportare.position = altroPortaleTransform.position;
        }
        
        Debug.Log(other.name + " è entrato");

    }


}
