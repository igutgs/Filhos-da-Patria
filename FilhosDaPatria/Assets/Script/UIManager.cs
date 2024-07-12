using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    static UIManager current;
    public TextMeshProUGUI UILife;

    public TextMeshProUGUI enemyCounterText; // Referência ao componente de texto
    private int defeatedEnemies = 0; // Contador de inimigos derrotados


    public TextMeshProUGUI gameOverText; // Referência ao texto de Game Over
    public GameObject gameOverPanel; // Painel de Game Over

    void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }

        current = this;

        DontDestroyOnLoad(gameObject);
    }

    public static void UpdateLifeUI(int life)
    {
        if (current == null)
            return;

        current.UILife.text = life.ToString();
    }

    public void IncrementCounter()
    {
        defeatedEnemies++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        enemyCounterText.text = "Inimigos Derrotados: " + defeatedEnemies;
    }

}
