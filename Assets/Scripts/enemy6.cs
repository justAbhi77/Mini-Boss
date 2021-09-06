using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy6 : MonoBehaviour
{
    [SerializeField]
    List<Transform> points;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform firepoint;

    [SerializeField]
    float health;

    [SerializeField]
    float speed, starttime, firetime;

    [SerializeField]
    bool isalive;

    [SerializeField]
    Animator animate;

    float waittime, firewait;
    int randompoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            damage();
    }

    void Start()
    {
        waittime = starttime;
        firewait = firetime;
        randompoint = Random.Range(0, points.Count);
        if (health == 0)
        {
            health = 1500;
        }
    }

    private void Update()
    {
        if (isalive)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[randompoint].position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, points[randompoint].position) < .2f)
            {
                if (waittime <= 0)
                {
                    randompoint = Random.Range(0, points.Count);
                    waittime = starttime;
                }
                else
                {
                    waittime -= Time.deltaTime;
                }
            }
            if (firewait <= 0)
            {
                animate.SetBool("atk", true);
                StartCoroutine(attack());
                Instantiate(bullet, firepoint);
                firewait = firetime;
            }
            else
                firewait -= Time.deltaTime;
        }
        if (health <= 0)
            isalive = false;
    }

    IEnumerator attack()
    {
        yield return new WaitForSeconds(.6f);
        animate.SetBool("atk", false);
    }

    public void damage()
    {
        health -= 100;
    }
}