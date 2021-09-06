using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class bullet : MonoBehaviour
{
    [SerializeField]
    float speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            try
            {
                collision.gameObject.GetComponent<enemy>().damage();
            }
            catch (Exception)
            {
                
            }
        Destroy(gameObject);
    }
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0)*Time.deltaTime;
        Destroy(gameObject, 2.5f);
    }
}
