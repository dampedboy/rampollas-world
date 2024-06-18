using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    // Velocità di rotazione in gradi al secondo
    public float rotationSpeed = 50.0f;
    public float angle = 90.0f;
    [SerializeField] Transform tasselloGirevoleTransform;

    public GameObject tasselloGirevole;
    public GameObject indicatore;

    public GameObject perno;

    private Material materialeIndicatore;

    [SerializeField] bool allineato=false;

    void Awake()
    {
        materialeIndicatore = indicatore.GetComponent<Renderer>().material;
        tasselloGirevoleTransform = tasselloGirevole.GetComponent<Transform>();
    }
    void Update()
    {
        // Calcola la rotazione per questo frame
        float rotation = rotationSpeed * Time.deltaTime;

        // Applica la rotazione alla piattaforma lungo l'asse Y
        
        tasselloGirevole.GetComponent<Transform>().transform.Rotate(0, 0, rotation);
        //perno.GetComponent<Transform>().eulerAngles.z = angle;


        CheckAllineato();
        if (!allineato)
        {
            materialeIndicatore.color = Color.red;
        }
        else
        {
            materialeIndicatore.color = Color.green;
        }
        //angle=tasselloGirevole.GetComponent<Transform>().rotation;
        Debug.Log("Angolo rotazione:"+tasselloGirevole.GetComponent<Transform>().eulerAngles.z);
    }

    void CheckAllineato()
    {
        allineato = (tasselloGirevoleTransform.eulerAngles.z <= angle + 10.0f && tasselloGirevoleTransform.eulerAngles.z >= angle - 10.0f) ?true:false;       
            
        
    }
}