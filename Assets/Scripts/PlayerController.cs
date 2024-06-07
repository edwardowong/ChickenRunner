using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;

    [Header("Movement")]
    public float speed = 20.0f;
    public float rotationSpeed = 720.0f;

    private Animator animator;
    public Transform player;
    public Transform orientation;
    private Rigidbody rb;

    public GameObject door1, door2, door3;
    public AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.playOnAwake = false;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        var moveDirection = orientation.transform.forward * verticalInput + orientation.transform.right * horizontalInput;
        var forward = orientation.transform.forward * verticalInput;
        //Animation
        if (moveDirection != Vector3.zero)
        {
            rb.AddForce(forward * speed * Time.deltaTime);
            player.forward = Vector3.Slerp(player.forward, moveDirection.normalized, Time.deltaTime * rotationSpeed);
            animator.SetBool("Run", true);
        }
        else
            animator.SetBool("Run", false);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Egg1")
        {
            Destroy(other.gameObject);
            Destroy(door1);
            audioSrc.Play();
        }
        else if (other.gameObject.name == "Egg2")
        {
            Destroy(other.gameObject);
            Destroy(door2);
            audioSrc.Play();
        }
        else if (other.gameObject.name == "Egg3")
        {
            Destroy(other.gameObject);
            Destroy(door3);
            audioSrc.Play();
        }

    }
}
