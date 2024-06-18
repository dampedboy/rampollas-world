using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portale : MonoBehaviour
{
    //public Collider oggettoDaTeletrasportare;
    public GameObject altroPortale;
     [SerializeField] string nomeOggetto="";
    // Start is called before the first frame update
    void Start()
    {
        //oggettoDaTeletrasportare = new Collider();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.GetComponent<Collider>().isTrigger+"is trigger");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Transform>())
        {

        }
        Debug.Log(other.name + " è entrato");
    }
}
