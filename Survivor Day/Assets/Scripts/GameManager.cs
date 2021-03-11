using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Para accader a elementos de la IU
using UnityEngine.UI;
// Para manejar escenas
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Text vidas;
    public Text monedas;
    public Text puntuacion;
    public Text record;

    public Personaje personaje;
    public GameObject gameOver;

    public AudioClip boton;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        this.gameOver.SetActive(false);
        this.monedas.text = "X " + PlayerPrefs.GetInt("Monedas").ToString();
        this.vidas.text = PlayerPrefs.GetInt("Vidas").ToString() + " X";
    }

    // Update is called once per frame
    void Update()
    {
        
        this.vidas.text = "X " + this.personaje.vidas.ToString();
        this.monedas.text = this.personaje.monedas.ToString() + " X";

        if (this.personaje.vidas <= 0 && !this.gameOver.activeSelf)
        {
            this.gameOver.SetActive(true);
            this.record.text = PlayerPrefs.GetInt("Monedas").ToString();
            this.puntuacion.text = this.personaje.monedas.ToString();
        }
    }

    public void BotonRestart()
    {
        SceneManager.LoadScene("Nivel 1");
    }

    public void BotonStart()
    {
        SceneManager.LoadScene("Inicio");
    }
    public void BotonAcercaDe()
    {
        SceneManager.LoadScene("Info Desarrollador");
    }
}
