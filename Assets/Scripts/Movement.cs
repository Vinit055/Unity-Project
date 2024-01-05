using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]float mainThrust = 2000f;
    [SerializeField]float rotationThrust = 150f;
    [SerializeField]AudioClip mainEngine;
    [SerializeField]ParticleSystem engineParticles;
    [SerializeField]ParticleSystem sideThrusterRightParticles;
    [SerializeField]ParticleSystem sideThrusterLeftParticles;
    
    Rigidbody rb;
    AudioSource audio;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessMovement();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            startThrusting();
        }
        else
        {
            stopThrusting();
        }
    }

    void ProcessMovement()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            rotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            rotateRight();
        }
        else
        {
            stopRotating();
        }
    }
    void startThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!audio.isPlaying)
            {
            audio.PlayOneShot(mainEngine);
            }
            if(!engineParticles.isPlaying)
            {
                engineParticles.Play();
            }
    }

    void stopThrusting()
    {
        audio.Stop();
        engineParticles.Stop();
    }

    

    void rotateLeft()
    {
        if(!sideThrusterRightParticles.isPlaying)
            {
                sideThrusterRightParticles.Play();
            }
    }

    void rotateRight()
    {
        if(!sideThrusterLeftParticles.isPlaying)
            {
                sideThrusterLeftParticles.Play();
            }
    }

    void stopRotating()
    {
        sideThrusterLeftParticles.Stop();
        sideThrusterRightParticles.Stop();
    }

    void ApplyRotation(float rotateThisFrame)
    {
       rb.freezeRotation = true;
       transform.Rotate(Vector3.forward * rotateThisFrame * Time.deltaTime);
       rb.freezeRotation = false;
    }

    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Finish")
        {
            rb.freezeRotation = true;
        }    
    }

    
}
