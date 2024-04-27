using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    new SpriteRenderer renderer;
    Color coolColor = new Color(0.2985047f, 0.3668804f, 0.8113208f);
    Color burnColor = new Color(0.3433962f, 0.3129441f, 0.3129441f);

    private void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ice"))
        {
            StartCoroutine(CoolPlayer());
        }
        else if (other.gameObject.CompareTag("Fire"))
        {
            StartCoroutine(BurnPlayer());

        }
    }

    IEnumerator CoolPlayer()
    {
        renderer.color = coolColor;

        yield return new WaitForSeconds(1f);

        renderer.color = Color.white;

    }

    IEnumerator BurnPlayer()
    {
        renderer.color = burnColor;

        yield return new WaitForSeconds(1f);

        renderer.color = Color.white;

    }
}
