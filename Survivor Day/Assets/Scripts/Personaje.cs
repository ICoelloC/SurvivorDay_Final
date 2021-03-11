using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Personaje : MonoBehaviour
{

    private bool saltando;

    public int vidas;
    public int monedas;

    public GameObject ataque;
    public GameObject posicionAtaque;

    public float velocidad = 0.2f;
    public float salto = 300.0f;

    public Animator animator;

    public bool Golpe = false;

    public bool mirandoDerecha;
    public bool mirandoIzquierda;

    private Rigidbody2D MyRb;

    private bool moviendoDerecha;
    private bool moviendoIzquierda;

    

    // Start is called before the first frame update
    void Start()
    {
        this.vidas = 1;
        this.monedas = 0;
        this.saltando = false;
        this.mirandoDerecha = true;
        this.mirandoIzquierda = false;
        MyRb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        this.animator.SetBool("Corriendo", false);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || this.moviendoIzquierda)
        {
            if (!mirandoDerecha)
            {
                girarPersonaje();
            }
            this.animator.SetBool("Corriendo", true);
            // Indicamos cuanto queremos que se mueva, no donde..., eje X
            transform.Translate(new Vector3(-this.velocidad, 0.0f));
        }
        

        // Si pulsamos ir a la derecha, o con los botones de la interfaz variable cerrojo
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || this.moviendoDerecha)
        {
            if (mirandoDerecha)
            {
                girarPersonaje();
            }
            this.animator.SetBool("Corriendo", true);
            // Indicamos cuanto queremos que se mueva, no donde... eje X
            transform.Translate(new Vector3(-this.velocidad, 0.0f));
        }

        // Si queremos saltar
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // saltamos, refactorizado a método propio para eventos de botones
            this.saltar();
        }
        
        if (Golpe)
        {
            MyRb.velocity = new Vector3(MyRb.velocity.x + velocidad, 0);
        }

    }


    // Nos detecta como actuar si hay colisión y choca una vez
    // Este método se llama cuando colisione con algo por primera vez, por eso filtramos con la etiqueta
    void OnCollisionEnter2D(Collision2D col)
    {
        // Comparamos si colisinamos con objetos con etiqueta Suelo
        if (col.gameObject.CompareTag("Suelo"))
        {
            this.saltando = false;
            // Animación
            this.animator.SetBool("Saltando", this.saltando);
        }

        // Si chocamos con un enemigo, lo quitamos y disminuye nuestra vida.
        
        if (col.gameObject.CompareTag("Enemigo"))
        {

            // Sonido
            //this.audioSource.PlayOneShot(this.sonidoDaño);
            col.gameObject.SetActive(false);
            Destroy(col.gameObject, 1.0f);
            this.vidas--;
            // animación
            this.animator.SetTrigger("Daño");
            Debug.Log("Vidas: " + this.vidas);
            // Si nos quedamos sin vidas
            // Cuando muedo
            if (this.vidas <= 0)
            {
                // Sonido
                //this.audioSource.PlayOneShot(this.sonidoMuerte);
                Debug.Log("El jugador ha muerto");
                // Animación
                this.animator.SetBool("Morir", true);
                // Comprobamos el record
                this.comprobarRecord();
                Destroy(this.gameObject, 0.5f);

            }
        }
        
        if (col.gameObject.tag == "PlataformaMovible")
        {
            transform.parent = col.transform;
            this.saltando = false;
            // Animación
            this.animator.SetBool("Saltando", this.saltando);
        }

    }

    // DEsactiva el personaje en X segundos
    void descativarPersonaje()
    {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 0.5f);
    }

    // Para los eventos de colisiones Triger
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es una moneda
        if (collision.gameObject.CompareTag("Moneda"))
        {
            // Sonido
            //this.audioSource.PlayOneShot(this.sonidoMoneda);
            // Nos cargamos la moneda
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.5f);
            // Aumentamos las monedas
            monedas++;
            Debug.Log("Monedas: " + this.monedas);
            if (monedas % 3 == 0)
            {
                vidas++;
                Debug.Log("Vidas: " + vidas);
                Debug.Log("Monedas: " + monedas);
            }
            
        }
        if (collision.gameObject.CompareTag("SueloMuerte"))
        {
            vidas = 0;

            if (this.vidas <= 0)
            {
                // Sonido
                //this.audioSource.PlayOneShot(this.sonidoMuerte);
                Debug.Log("El jugador ha muerto");
                // Animación
                this.animator.SetBool("Morir", true);
                // Comprobamos el record
                this.comprobarRecord();
                this.descativarPersonaje();

            }
            descativarPersonaje();
        }

        if (collision.gameObject.CompareTag("FinNivel1"))
        {
            SceneManager.LoadScene("Nivel 2");
        }

        if (collision.gameObject.CompareTag("FinNivel2"))
        {
            SceneManager.LoadScene("Info Desarrollador");
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaMovible")
        {
            transform.parent = null;
        }
    }

    private void comprobarRecord()
    {
        // PlayerPrefs nos deja guardar preferencias o datos en modo clave valor
        // Podríamos guardar estadísticas y recuperarlas
        int recordUltimo = PlayerPrefs.GetInt("Monedas");
        if (PlayerPrefs.HasKey("Monedas") == false)
        {
            //No hay record guardado
            PlayerPrefs.SetInt("Monedas", monedas);
        }
        else
        {
            //Si hay record guardado
            if (recordUltimo < monedas)
            {
                PlayerPrefs.SetInt("Monedas", monedas);
                Debug.Log("NUEVO RECORD! " + monedas);
            }
        }
    }

    public void saltar()
    {
        
        if (this.saltando == false)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, this.salto));
            this.saltando = true;
            this.animator.SetBool("Saltando", this.saltando);
            // Sonido
            //this.audioSource.clip = this.sonidoSalto;
            //this.audioSource.Play();
            //this.audioSource.PlayOneShot(this.sonidoSalto);
        }
    }

    void girarPersonaje()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.Rotate(0f,180f,0f);
    }


    public void moverDerecha(bool activar)
    {
        this.moviendoDerecha = activar;
    }

    // Cerrojo de movimiento izquierda
    public void moverIzquierda(bool activar)
    {
        this.moviendoIzquierda = activar;
    }

}
