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

    public AudioClip jumpSound;
    public AudioClip walkSound;
    private AudioSource audioSource;

    public bool launchable = false;
    private float ySpeed;
    private Animator animator;
    private float originalStepOffset;
    private CharacterController characterController;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    public bool isGrounded;
    public bool isAbsorbing = false;
    public Transform spawnPoint;
    public GameObject vortexPrefab;
    private Vector3 offset = new Vector3(0.25f, -0.8f, 0f);
    private GameObject vortexInstance;
    private Vector3 initialVortexPosition;
    public AudioClip assorbimento;

    public ObjAbsorber keyAbsorber;
    public ObjAbsorber keyAbsorber2;
    public ObjAbsorber obj1;
    public ObjAbsorber obj2;
    public ObjAbsorber obj3;
    public ObjAbsorber obj4;

    private bool hasAbsorbedObject = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        bool isLaunching = CheckIfLaunching();

        animator.SetBool("isLaunching", isLaunching);

        // Aspirazione/sputo con pulsante Fire3 o tasto O
        if (Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.O))
        {
            StartAbsorbing();
        }

        if (Input.GetButtonUp("Fire3") || Input.GetKeyUp(KeyCode.O))
        {
            StopAbsorbing();
        }

        if (isAbsorbing)
        {
            return;
        }

        HandleMovement();
    }

    private bool CheckIfLaunching()
    {
        return (keyAbsorber != null && keyAbsorber.isLaunching) ||
               (keyAbsorber2 != null && keyAbsorber2.isLaunching) ||
               (obj1 != null && obj1.isLaunching) ||
               (obj2 != null && obj2.isLaunching) ||
               (obj3 != null && obj3.isLaunching) ||
               (obj4 != null && obj4.isLaunching);
    }

    private void StartAbsorbing()
    {
        bool perfectPosition = CheckPerfectPosition();

        if (!perfectPosition && isGrounded && !launchable)
        {
            isAbsorbing = true;
            animator.SetBool("isAbsorbing", true);

            // Creazione del vortice
            if (vortexPrefab != null && spawnPoint != null)
            {
                Vector3 spawnPosition = spawnPoint.position + offset;
                Quaternion rotation = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, 90, 90);
                vortexInstance = Instantiate(vortexPrefab, spawnPosition, rotation);

                if (assorbimento != null)
                {
                    audioSource.PlayOneShot(assorbimento);
                }

                StartCoroutine(MakeTornadoVisible(vortexInstance));
            }
        }
    }

    private void StopAbsorbing()
    {
        isAbsorbing = false;
        animator.SetBool("isAbsorbing", false);

        // Se esiste un vortice, distruggilo
        if (vortexInstance != null)
        {
            Destroy(vortexInstance);
            vortexInstance = null; // Assicurati che l'istanza sia null dopo la distruzione
        }

        // Gestione del lancio
        if (obj1.isHoldingObject || obj2.isHoldingObject || obj3.isHoldingObject || obj4.isHoldingObject || keyAbsorber.isHoldingObject || keyAbsorber2.isHoldingObject)
        {
            launchable = true;
        }
    }

    private bool CheckPerfectPosition()
    {
        return (keyAbsorber == null || keyAbsorber.PerfectPosition) &&
               (keyAbsorber2 == null || keyAbsorber2.PerfectPosition) &&
               (obj1 == null || obj1.PerfectPosition) &&
               (obj2 == null || obj2.PerfectPosition) &&
               (obj3 == null || obj3.PerfectPosition) &&
               (obj4 == null || obj4.PerfectPosition);
    }

    private void HandleMovement()
    {
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

        if (isJumping && ySpeed > 0 && !Input.GetButton("Jump") && !Input.GetKey(KeyCode.Space))
        {
            gravity *= 2;
        }
        ySpeed += gravity * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
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

            if (audioSource.isPlaying && audioSource.clip == walkSound)
            {
                audioSource.Stop();
            }
        }
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
    public void BoostJump(float boostForce)
    {
        
        
            ySpeed = Mathf.Sqrt(boostForce * -3f * Physics.gravity.y);
            isJumping = true;
            animator.SetBool("isJumping", true);
            
        
    }
}