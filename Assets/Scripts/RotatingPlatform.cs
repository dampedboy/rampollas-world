using System;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    private const float tolerance = 20.0f;

    // Velocità di rotazione in gradi al secondo
    public float rotationSpeed = 0.0f;
    public float angoloRotazione = 0.0f;

    public float tempoDiReset =10.0f;

    //Tassello da far girare
    [SerializeField] Transform tasselloGirevoleTransform;

    public GameObject tasselloGirevole;
    public GameObject indicatore;

    public GameObject perno;
    public Transform pernoTransform;

    private Material materialeIndicatore;

    [SerializeField] bool allineato=false;
    public bool rotazioneAttiva=true;

    void Awake()
    {
        materialeIndicatore = indicatore.GetComponent<Renderer>().material;
        tasselloGirevoleTransform = tasselloGirevole.GetComponent<Transform>();
        pernoTransform = perno.GetComponent<Transform>();
    }
    void Update()
    {
        // Calcola la rotazione per questo frame
        float rotation = rotationSpeed * Time.deltaTime;

        // Applica la rotazione alla piattaforma lungo l'asse Z
        //tasselloGirevole.GetComponent<Transform>().transform.Rotate(0, 0, rotation);
        //perno.GetComponent<Transform>().eulerAngles.z = angle;

        RotazionePerno();
        CheckCondizioneDiCambiamento();
        
        CheckAllineato();
       
        //Debug.Log("Angolo rotazione tassello:"+ tasselloGirevoleTransform.eulerAngles.z);
    }

    private void CheckCondizioneDiCambiamento()
    {
        if (rotazioneAttiva)
        {
            RotazioneTassello();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            rotazioneAttiva = true;
        }
        Debug.Log(other.name+" è sulla piattaforma");
    }
    void RotazioneTassello()
    {
        float differenzaAngolo = tasselloGirevoleTransform.eulerAngles.z - pernoTransform.eulerAngles.z;
        tasselloGirevoleTransform.Rotate(0, 0, ((differenzaAngolo>0) ? -1 : 1) * differenzaAngolo *rotationSpeed*Time.deltaTime);
    }
    private float RotazionePerno()
    {
        pernoTransform.eulerAngles=new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,angoloRotazione);
        return pernoTransform.eulerAngles.z;
    }
    void CheckAllineato()
    {
        allineato = (tasselloGirevoleTransform.eulerAngles.z <= angoloRotazione + tolerance && tasselloGirevoleTransform.eulerAngles.z >= angoloRotazione - tolerance) ?true:false;       
            
         if (!allineato)
        {
            materialeIndicatore.color = Color.red;
            materialeIndicatore.EnableKeyword("_EMISSION");
            materialeIndicatore.SetColor("_EmissionColor", new Color(0,0,0));
        }
        else
        {
            materialeIndicatore.color = Color.green;
            materialeIndicatore.EnableKeyword("_EMISSION");
            materialeIndicatore.SetColor("_EmissionColor", Color.Lerp(materialeIndicatore.color,Color.green,100.0f*Time.deltaTime));
        }
    }
}