using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CookieController : MonoBehaviour
{
    public Scrollbar HealthBar;
    public SoundManager soundManager;
    public Text ScoreText;

    private Rigidbody2D cookie;
    public PolygonCollider2D cookieIdle;
    public PolygonCollider2D cookieSlide;
    private Animator cookieAni;
    private const float POWER = 25.0f;
    private bool jump = false;
    private bool slide = false;
    private int jumpCount = 0;
    private int Score = 0;
    private float health = 100f;

    private void Start()
    {
        cookie = GetComponent<Rigidbody2D>();
        cookieAni = GetComponent<Animator>();
        ScoreRenew();
        LifeRenew();
        Invoke("Scenechange", 80);

        StartCoroutine("NextPage");
    }

    private void FixedUpdate()
    {
        if (jumpCount > 0 && jump)
        {
            cookie.velocity = (Vector2.up * POWER);
            cookieAni.SetInteger("Jump", jumpCount);
            jump = false;
        }
    }

    private void Update()
    {
        health -= 0.02f;
        LifeRenew();
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cookieAni.SetBool("Slide", true);
            slide = true;
            cookieSlide.enabled = true;
            cookieIdle.enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            cookieAni.SetBool("Slide", false);
            slide = false;
            cookieIdle.enabled = true;
            cookieSlide.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2 && !slide)
        {
            jumpCount++;
            jump = true;
            cookieAni.SetBool("Idle", false);
        }


        if (0 >= HealthBar.size)
        {
            cookieAni.SetBool("Die", true);
            GameManager.instance.gameOver = true;
        }

        if (transform.position.y < -10)
        {
            GameManager.instance.gameOver = true;
        }

        if (GameManager.instance.gameOver)
        {
            SceneManager.LoadScene("LastP");
            LifeRenew();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            cookieAni.SetInteger("Jump", jumpCount);
            cookieAni.SetBool("Idle", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Potion"))
        {
            health += 5f;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Trap"))
        {
            cookieAni.SetBool("Crash", true);
            health -= 10;
            LifeRenew();
        }

        if (collision.CompareTag("Pit"))
        {
            GameManager.instance.gameOver = true;
        }

        if (collision.CompareTag("Coin"))
        {
            Score++;
            ScoreRenew();
            soundManager.PlayEatSound();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Crystal"))
        {
            Score += 10;
            ScoreRenew();
            soundManager.PlayEatSound();
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            cookieAni.SetBool("Crash", false);
        }
    }

    void LifeRenew()
    {
        HealthBar.size = health / 100f;
    }

    void ScoreRenew()
    {
        ScoreText.text = "Score : " + Score.ToString();
    }

    void Scenechange()
    {
        SceneManager.LoadScene("CLass");
    }

    IEnumerator NextPage()
    {
        while (true)
        {
            yield return new WaitForSeconds(15.0f);
            GameManager.instance.stage++;
        }
    }
}
