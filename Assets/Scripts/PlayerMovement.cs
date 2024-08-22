using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maximumSpeed;
    public float rotationSpeed;
    public float jumpHeight;
    public float gravityMultiplier;
    public float jumpButtonGracePeriod;

    public AudioClip jumpSound; // Suono per il salto
    public AudioClip walkSound; // Suono per la camminata
    private AudioSource audioSource;

    // ySpeed is needed for jump gravity
    private float ySpeed;
    private Animator animator;
    private float originalStepOffset;
    private CharacterController characterController;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    private bool isGrounded;
    private bool isAbsorbing = false; // Variabile per lo stato di assorbimento
    public Transform spawnPoint; // Riferimento al Transform del tappo
    public GameObject vortexPrefab; // Il prefab del vortice
    public Vector3 offset;
    private GameObject vortexInstance;
    private Vector3 initialVortexPosition;
    public AudioClip assorbimento; // Audio clip per il suono di assorbimento

    public ObjAbsorber keyAbsorber; // Riferimento al KeyAbsorber
    public ObjAbsorber keyAbsorber2; // Riferimento al KeyAbsorber
    public ObjAbsorber obj1; // riferimento agli oggetti di legno 
    public ObjAbsorber obj2; // riferimento agli oggetti di vetro 
    public ObjAbsorber obj3; // riferimento agli oggetti di metallo
    public ObjAbsorber obj4; // riferimento agli oggetti di metallo

    // GameObjects da mostrare/nascondere
    public GameObject empty;
    public GameObject key;
    public GameObject dynamite;
    public GameObject metal;
    public GameObject glass;
    public GameObject wood;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        audioSource = GetComponent<AudioSource>(); // Carica il componente AudioSource

        // Inizialmente mostra solo l'oggetto 'empty'
        SetObjectVisibility(empty, true);
        SetObjectVisibility(key, false);
        SetObjectVisibility(dynamite, false);
        SetObjectVisibility(metal, false);
        SetObjectVisibility(glass, false);
        SetObjectVisibility(wood, false);
    }

    private IEnumerator ResetAbsorbing()
    {
        yield return new WaitForSeconds(0.3f);
        if (vortexPrefab != null && spawnPoint != null)
        {
            Vector3 spawnPosition = spawnPoint.position + offset;
            Quaternion rotation = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, 90, 90);
            // Instanzia e salva il riferimento all'istanza del vortice
            vortexInstance = Instantiate(vortexPrefab, spawnPosition, rotation);
            if (assorbimento != null)
            {
                audioSource.PlayOneShot(assorbimento);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isLaunching = false;

        // Controlla se ci sono oggetti da lanciare
        if (keyAbsorber != null && keyAbsorber.isLaunching)
        {
            isLaunching = true;
        }
        if (keyAbsorber2 != null && keyAbsorber2.isLaunching)
        {
            isLaunching = true;
        }
        if (obj4 != null && obj4.isLaunching)
        {
            isLaunching = true;
        }
        if (obj1 != null && obj1.isLaunching)
        {
            isLaunching = true;
        }
        if (obj2 != null && obj2.isLaunching)
        {
            isLaunching = true;
        }
        if (obj3 != null && obj3.isLaunching)
        {
            isLaunching = true;
        }

        animator.SetBool("isLaunching", isLaunching);

        // Aspirazione/sputo con pulsante sinistro
        if (Input.GetButtonDown("Fire3")) // Cambia "Fire2" con l'azione che vuoi utilizzare per il pulsante sinistro
        {
            bool perfectPosition = true;

            if (keyAbsorber != null && !keyAbsorber.PerfectPosition)
            {
                perfectPosition = false;
            }
            if (keyAbsorber2 != null && !keyAbsorber2.PerfectPosition)
            {
                perfectPosition = false;
            }

            if (obj4 != null && !obj4.PerfectPosition)
            {
                perfectPosition = false;
            }

            if (obj1 != null && !obj1.PerfectPosition)
            {
                perfectPosition = false;
            }

            if (obj2 != null && !obj2.PerfectPosition)
            {
                perfectPosition = false;
            }

            if (obj3 != null && !obj3.PerfectPosition)
            {
                perfectPosition = false;
            }

            if (!perfectPosition && isGrounded)
            {
                isAbsorbing = true;
                animator.SetBool("isAbsorbing", true);

                // Crea il vortice istantaneamente
                if (vortexPrefab != null && spawnPoint != null)
                {
                    Vector3 spawnPosition = spawnPoint.position + offset;
                    Quaternion rotation = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, 90, 90);
                    vortexInstance = Instantiate(vortexPrefab, spawnPosition, rotation);

                    if (assorbimento != null)
                    {
                        audioSource.PlayOneShot(assorbimento);
                    }

                    // Avvia la coroutine per rendere il tornado visibile dopo 0,3 secondi
                    StartCoroutine(MakeTornadoVisible(vortexInstance));
                }

                // Controlla quale oggetto è stato assorbito e aggiorna la visibilità dei GameObject
                if (keyAbsorber.PerfectPosition && keyAbsorber.CompareTag("Key"))
                {
                    SetObjectVisibility(key, true);
                }
                else if (keyAbsorber2.PerfectPosition && keyAbsorber2.CompareTag("Dynamite"))
                {
                    SetObjectVisibility(dynamite, true);
                }
                else if (obj4.PerfectPosition && obj4.CompareTag("Metal"))
                {
                    SetObjectVisibility(metal, true);
                }
                else if (obj1.PerfectPosition && obj1.CompareTag("Wood"))
                {
                    SetObjectVisibility(wood, true);
                }
                else if (obj2.PerfectPosition && obj2.CompareTag("Glass"))
                {
                    SetObjectVisibility(glass, true);
                }
                else if (obj3.PerfectPosition && obj3.CompareTag("Metal"))
                {
                    SetObjectVisibility(metal, true);
                }
            }
        }

        if (Input.GetButtonUp("Fire3"))
        {
            isAbsorbing = false;
            animator.SetBool("isAbsorbing", false);

            // Elimina il vortice immediatamente
            if (vortexInstance != null)
            {
                Destroy(vortexInstance);
                vortexInstance = null;
            }
        }

        // Blocca il movimento se il personaggio è in stato di assorbimento
        if (isAbsorbing)
        {
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude /= 2;
        }

        animator.SetFloat("input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);
        float speed = inputMagnitude * maximumSpeed;
        movementDirection.Normalize();

        float gravity = Physics.gravity.y * gravityMultiplier;

        // Salto con pulsante in basso
        if (isJumping && ySpeed > 0 && Input.GetButton("Fire1") == false)
        {
            gravity *= 2;
        }
        ySpeed += gravity * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            animator.SetBool("isGrounded", true);
            isGrounded = true;
            animator.SetBool("isJumping", false);
            isJumping = false;
            animator.SetBool("isFalling", false);

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = Mathf.Sqrt(jumpHeight * -3 * gravity);
                animator.SetBool("isJumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;

                // Riproduci il suono del salto
                audioSource.PlayOneShot(jumpSound);
            }
        }
        else
        {
            characterController.stepOffset = 0;
            animator.SetBool("isGrounded", false);
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                animator.SetBool("isFalling", true);
            }
        }

        Vector3 velocity = movementDirection * speed;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);

            // Riproduci il suono della camminata se il giocatore è a terra e si sta muovendo
            if (isGrounded && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(walkSound);
            }

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);

            // Interrompi il suono della camminata se il giocatore si ferma
            if (audioSource.isPlaying && audioSource.clip == walkSound)
            {
                audioSource.Stop();
            }
        }
    }

    public void Jump(float jumpForce)
    {
        ySpeed = Mathf.Sqrt(jumpForce * -2 * Physics.gravity.y * gravityMultiplier);
        animator.SetBool("isJumping", true);
        isJumping = true;
        jumpButtonPressedTime = null;
        lastGroundedTime = null;
    }

    private IEnumerator MakeTornadoVisible(GameObject tornado)
    {
        // Imposta il tornado come invisibile disabilitando i componenti di rendering
        SetTornadoVisibility(tornado, false);

        // Attendi per 0,3 secondi
        yield return new WaitForSeconds(0.3f);

        // Rendi il tornado visibile
        SetTornadoVisibility(tornado, true);
    }

    private void SetTornadoVisibility(GameObject tornado, bool visible)
    {
        Renderer[] renderers = tornado.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = visible;
        }
    }

    private void SetObjectVisibility(GameObject obj, bool visible)
    {
        if (obj != null)
        {
            obj.SetActive(visible);
        }
    }
}
