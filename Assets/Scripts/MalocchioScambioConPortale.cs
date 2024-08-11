using System.Collections;
using UnityEngine;

public class MalocchioScambioConPortale : MonoBehaviour
{

    public GameObject malocchio; // Riferimento a Malocchio
    public GameObject portal; // Riferimento all'oggetto Portal

    void Start()
    {
        portal.SetActive(false); // Rende il portale invisibile all'inizio
    }




    void CambioScena(Collider other)
    {

     Destroy(gameObject); // Distruggi l'oggetto chiave
     Destroy(malocchio); // Distruggi l'oggetto Malocchio
     portal.SetActive(true); // Rendi il portale visibile

    }


}