using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offscreen : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            try
            {
                collision.gameObject.GetComponent<PlayerMovement>().die();
                collision.gameObject.transform.position = new Vector3(-5f, 1.6f, 0);
            }
            catch (System.Exception)
            {

            }
    }
}
