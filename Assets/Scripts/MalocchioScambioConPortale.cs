using System.Collections;
using UnityEngine;

public class MalocchioScambioConPortale : MonoBehaviour
{

    public GameObject malocchio; // Riferimento a Malocchio
    public GameObject portal; // Riferimento all'oggetto Portal
    public GameObject canvaTalk; // Riferimento all'oggetto canvaTalk

    void Start()
    {
        portal.SetActive(false); // Rende il portale invisibile all'inizio
    }




    public void CambioScena()
    {

     Destroy(gameObject); // Distruggi l'oggetto chiave
     Destroy(malocchio); // Distruggi l'oggetto Malocchio
     Destroy(canvaTalk); // Distruggi l'oggetto canvaTalk

        portal.SetActive(true); // Rendi il portale visibile

    }


}