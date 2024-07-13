using UnityEngine;
using System.Collections;

public class MetalBlock : MonoBehaviour
{
    public GameObject woodBlockPrefab;

    private bool canCollide = true; // Variabile per controllare se il blocco può collidere

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        // Controlla se il blocco può collidere e se l'oggetto che ha causato la collisione è di metallo
        if (canCollide && otherObject.CompareTag("Metal"))
        {
            // Ottieni lo script ObjAbsorbeMetal dall'oggetto che ha causato la collisione
            ObjAbsorbeMetal metalScript = otherObject.GetComponent<ObjAbsorbeMetal>();

            // Controlla se l'oggetto di metallo è stato avvicinato al player
            if (metalScript != null && metalScript.isThrown)
            {
                ReplaceWithWood();
            }
        }
    }

    private void ReplaceWithWood()
    {
        // Disabilita ulteriori collisioni per evitare che altre istanze di MetalBlock siano sostituite immediatamente
        canCollide = false;

        // Ottieni la posizione e la rotazione del blocco di metallo
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        // Sostituisci con un nuovo blocco di legno dopo un ritardo
        StartCoroutine(ReplaceAfterDelay(position, rotation));
    }

    private IEnumerator ReplaceAfterDelay(Vector3 position, Quaternion rotation)
    {
        // Attendi un certo periodo prima di sostituire con il blocco di legno
        yield return new WaitForSeconds(1f); // Modifica il valore 1f a seconda del ritardo desiderato

        // Sostituisci con un nuovo blocco di legno
        Instantiate(woodBlockPrefab, position, rotation);

        // Distruggi questo blocco di metallo
        Destroy(gameObject);
    }
}