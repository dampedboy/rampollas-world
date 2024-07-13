
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]


public class DialogueObject : ScriptableObject
{
    [SerializeField][TextArea] private string[] dialogue;
    //per le opzioni:
    [SerializeField] private Response[] responses;



    public string[] Dialogue => dialogue;

    public bool HasResponses => Responses != null && Responses.Length > 0;


    //per le opzioni:
    public Response[] Responses => responses;
}
