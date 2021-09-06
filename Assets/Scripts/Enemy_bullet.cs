using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_bullet : MonoBehaviour
{
    [SerializeField]
    float speed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0) * Time.deltaTime * -1;
        Destroy(gameObject, 2.5f);
    }
}
