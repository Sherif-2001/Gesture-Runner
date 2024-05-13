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
    Color cactusColor = new Color(0.2984478f, 0.6301887f, 0.3041549f);

    [SerializeField] List<Image> livesObjects;
    int hits = 0;

    bool isGrounded = false;
    private Rigidbody2D playerBody;
    [SerializeField] float jumpPower = 10f;

    private ScoreManager scoreManager;



    private void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        scoreManager = GameObject.Find("Game Manager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            scoreManager.IncreaseScore();
        }
        else
        {
            StartCoroutine(HitEffect(other.gameObject));
        }
    }

    IEnumerator HitEffect(GameObject obstacle)
    {
        switch (obstacle.tag)
        {
            case "Ice":
                renderer.color = coolColor;
                break;

            case "Fire":
                renderer.color = burnColor;
                break;

            case "Cactus":
                renderer.color = cactusColor;
                break;
        }

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
            scoreManager.SavePlayerScore();
            return;
        }
    }

    public void Jump()
    {
        GameObject[] cactus = GameObject.FindGameObjectsWithTag("Cactus");
        if (isGrounded && cactus.Length != 0)
        {
            playerBody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
