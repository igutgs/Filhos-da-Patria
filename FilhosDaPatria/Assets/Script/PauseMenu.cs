using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Transform Tutorial;
    public Transform pauseMenu;
    public Transform Opcoes;

    private void Start()
    {
        Time.timeScale = 0;
        Tutorial.gameObject.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void Jogar()
    {
        Tutorial.gameObject.SetActive(false);
        Time.timeScale = 1;
        Health healthComponent = GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.health += 5; // Adiciona 5 pontos de vida
        }
    }

    public void Continuar()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpcoesEntrar()
    {
        pauseMenu.gameObject.SetActive(false);
        Opcoes.gameObject.SetActive(true);
    }

    public void OpcoesSair()
    {
        Opcoes.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);   
    }

    public void SairJogo()
    {
        Application.Quit();
    }
}
