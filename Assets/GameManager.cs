using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variabile statica per memorizzare lo stato di ingresso
    public static bool enteringFromTrapdoor = false;
    public static bool enteringFromColor = false;
    public static bool enteringFromAction = false;
    public static bool enteringFromBreak = false;

    public static bool enteringFromButton = false;
    public static bool enteringFromPortal = false;


    private static GameManager instance;
    
     private void Awake()
    {
        // Assicurati che ci sia solo una istanza di GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Non distruggere questo oggetto quando si cambia scena
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Metodo per impostare l'ingresso dalla botola
    public static void EnterFromTrapdoor()
    {
        enteringFromTrapdoor = true;
    }

    // Metodo per impostare l'ingresso dalla porta
    public static void EnterFromDoor()
    {
        enteringFromTrapdoor = false;
    }
    public static void EnterFromColor()
    {
        enteringFromColor = true;
        enteringFromAction = false;
        enteringFromBreak = false;
        enteringFromButton = false;
        enteringFromPortal = false;
    }

    public static void EnterFromAction()
    {
        enteringFromColor = false;
        enteringFromAction = true;
        enteringFromBreak = false;
        enteringFromButton = false;
        enteringFromPortal = false;
    }
    public static void EnterFromBreak()
    {
        enteringFromColor = false;
        enteringFromAction = false;
        enteringFromBreak = true;
        enteringFromButton = false;
        enteringFromPortal = false;
    }
    public static void EnterFromButton()
    {
        enteringFromColor = false;
        enteringFromAction = false;
        enteringFromBreak = false;
        enteringFromButton = true;
        enteringFromPortal = false;
    }
    public static void EnterFromPortal()
    {
        enteringFromColor = false;
        enteringFromAction = false;
        enteringFromBreak = false;
        enteringFromButton = false;
        enteringFromPortal = true;
    }
    public static void  JustNo()
    {
        enteringFromColor = false;
        enteringFromAction = false;
        enteringFromBreak = false;
        enteringFromButton = false;
        enteringFromPortal = false;
    }
}