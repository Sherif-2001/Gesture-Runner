using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    [SerializeField] GameObject iceParticles;
    [SerializeField] GameObject fireParticles;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            print("Key 1");
            GameObject ice = GameObject.FindGameObjectWithTag("Ice");
            GameObject particles = Instantiate(iceParticles, ice.transform.position, Quaternion.identity);
            particles.GetComponent<ParticleSystem>().Play();
            Destroy(ice);

        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            print("Key 2");
            GameObject fire = GameObject.FindGameObjectWithTag("Fire");
            GameObject particles = Instantiate(fireParticles, fire.transform.position, Quaternion.identity);
            particles.GetComponent<ParticleSystem>().Play();
            Destroy(fire);
        }
    }


}
