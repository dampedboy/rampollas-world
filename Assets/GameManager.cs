using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variabile statica per memorizzare lo stato di ingresso
    public static bool enteringFromTrapdoor = false;
    private static GameManager instance;
    
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
}