using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{
    public AudioClip musicaInicio;
    private AudioSource audioSource;
    void Start()
    {
        this.audioSource = this.GetComponent<AudioSource>();
    }

    public void BotonStart()
    {
        SceneManager.LoadScene("Nivel 1");
    }

    public void BotonVolver()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void BotonAcercaDe()
    {
        // Le decimos a que escena queremos ir
        SceneManager.LoadScene("Info Desarrollador");
    }
}
