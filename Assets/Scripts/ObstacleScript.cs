using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    ParticleSystem particles;

    void Start()
    {
        particles = GameObject.Find($"{gameObject.tag} Particles").GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            particles.Play();
            Destroy(gameObject);
        }
    }
}
