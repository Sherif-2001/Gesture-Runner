using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    new SpriteRenderer renderer;
    Color coolColor = new Color(0.2985047f, 0.3668804f, 0.8113208f);
    Color burnColor = new Color(0.3433962f, 0.3129441f, 0.3129441f);

    [SerializeField] List<Image> livesObjects;
    int hits = 0;

    private void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(HitEffect(other.gameObject));
    }

    IEnumerator HitEffect(GameObject obstacle)
    {
        renderer.color = obstacle.CompareTag("Ice") ? coolColor : burnColor;
        Hurt();

        yield return new WaitForSeconds(1f);

        renderer.color = Color.white;

    }

    void Hurt()
    {
        livesObjects[hits].color = Color.black;
        hits++;

        if (hits >= 3)
        {
            print("Die");
            return;
        }
    }
}
