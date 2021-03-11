using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{

    public Transform PuntoA;
    public Transform PuntoB;

    public bool MoveToA = false;
    public bool MoveToB = false;

    public float speed;

    private Rigidbody2D MyRb;

    public bool mirandoDerecha;

    public int tipo_mov;

    private Personaje personaje;

    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        MyRb = GetComponent<Rigidbody2D>();
        personaje = FindObjectOfType<Personaje>();
        MoveToA = true;
        MoveToB = false;

    }

    // Update is called once per frame
    void Update()
    {


        if (MoveToA)
        {
            if (!mirandoDerecha)
            {
                girarPersonaje();
            }
            MyRb.transform.position = Vector3.MoveTowards(transform.position, PuntoA.position, speed * Time.deltaTime);
            if (transform.position == PuntoA.position)
            {
                MoveToA = false;
                MoveToB = true;
            }
        }

        if (MoveToB)
        {
            if (mirandoDerecha)
            {
                girarPersonaje();
            }
            MyRb.transform.position = Vector3.MoveTowards(transform.position, PuntoB.position, speed * Time.deltaTime);
            if (transform.position == PuntoB.position)
            {
                MoveToA = true;
                MoveToB = false;
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Personaje"){
            personaje.Golpe = true;
            Destroy(gameObject);
            personaje.Golpe = false;
        }
    }

    public void girarPersonaje()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.Rotate(0f, 180f, 0f);
    }

}
