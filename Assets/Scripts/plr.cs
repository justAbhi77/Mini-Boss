using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class plr : MonoBehaviour
{
    [SerializeField]
    float speed,shoot_time,lives;

    [SerializeField]
    bool canshoot,isalive;

    [SerializeField]
    Transform firepoint;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    GameObject[] lives_gameobject;

    Vector2 screensize;
    Rigidbody2D rb2d;

    void Start()
    {
        if (lives > 0)
            isalive = true;
        canshoot = true;
        screensize = new Vector2(
            Camera.main.aspect * Camera.main.orthographicSize - transform.localScale.x / 2,
            Camera.main.orthographicSize - transform.localScale.y / 2);
        rb2d = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb2d.velocity = new Vector2(x, y) * speed * Time.deltaTime;

        if (transform.position.x < -screensize.x)
            transform.position = new Vector2(-screensize.x, transform.position.y);
        else if (transform.position.x > screensize.x)
            transform.position = new Vector2(screensize.x, transform.position.y);
        if (transform.position.y < -screensize.y)
            transform.position = new Vector2(transform.position.x, -screensize.y);
        else if (transform.position.y > screensize.y)
            transform.position = new Vector2(transform.position.x, screensize.y);

        if(canshoot && Input.GetButton("Fire1"))
        {
            canshoot = false;
            StartCoroutine(shoot());
        }

        if(isalive)
        {
            if (lives > 4)
                lives = 4;
            switch (lives)
            {
                case 4:
                    lives_gameobject[0].SetActive(true);
                    lives_gameobject[1].SetActive(true);
                    lives_gameobject[2].SetActive(true);
                    lives_gameobject[3].SetActive(true);
                    break;
                case 3:
                    lives_gameobject[0].SetActive(true);
                    lives_gameobject[1].SetActive(true);
                    lives_gameobject[2].SetActive(true);
                    lives_gameobject[3].SetActive(false);
                    break;
                case 2:
                    lives_gameobject[0].SetActive(true);
                    lives_gameobject[1].SetActive(true);
                    lives_gameobject[2].SetActive(false);
                    lives_gameobject[3].SetActive(false);
                    break;
                case 1:
                    lives_gameobject[0].SetActive(true);
                    lives_gameobject[1].SetActive(false);
                    lives_gameobject[2].SetActive(false);
                    lives_gameobject[3].SetActive(false);
                    break;
                case 0:
                    lives_gameobject[0].SetActive(false);
                    lives_gameobject[1].SetActive(false);
                    lives_gameobject[2].SetActive(false);
                    lives_gameobject[3].SetActive(false);
                    isalive = false;
                    Debug.LogWarning("Dead");
                    break;
            }
        }
    }

    IEnumerator shoot()
    {
        Instantiate(bullet, firepoint);
        yield return new WaitForSeconds(shoot_time);
        canshoot = true;
    }

    public void die()
    {
        lives -= 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            die();
    }
}
