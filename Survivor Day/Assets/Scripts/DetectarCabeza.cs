using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarCabeza : MonoBehaviour
{

    GameObject Enemigo;

    void Start()
    {
        Enemigo = gameObject.transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Collider2D>().enabled = false;
        Enemigo.GetComponent<SpriteRenderer>().flipX = true;
        Enemigo.GetComponent<Collider2D>().enabled = false;
    }
}
