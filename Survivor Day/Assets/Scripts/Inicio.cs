using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{
    public AudioClip musicaInicio;
    public AudioClip boton;
    private AudioSource audioSource;
    void Start()
    {
        this.audioSource = this.GetComponent<AudioSource>();
    }

    public void BotonStart()
    {
        this.audioSource.PlayOneShot(this.boton);
        SceneManager.LoadScene("Nivel 1");
    }

    public void BotonVolver()
    {
        this.audioSource.PlayOneShot(this.boton);
        SceneManager.LoadScene("Inicio");
    }

    public void BotonAcercaDe()
    {
        this.audioSource.PlayOneShot(this.boton);
        SceneManager.LoadScene("Info Desarrollador");
    }
}
