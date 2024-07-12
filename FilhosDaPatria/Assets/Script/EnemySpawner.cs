using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab do inimigo
    public Transform spawnPoint1; // Ponto de spawn 1
    public Transform spawnPoint2; // Ponto de spawn 2
    public float timeBetweenRounds = 5f; // Tempo entre cada rodada
    public UIManager enemyCounterUI; // Referência ao script de UI

    private int currentRound = 1; // Rodada atual
    private List<GameObject> activeEnemies = new List<GameObject>(); // Lista de inimigos ativos

    void Update()
    {
        if (activeEnemies.Count == 0)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < currentRound; i++)
        {
            // Escolha aleatoriamente um dos dois pontos de spawn
            Transform chosenSpawnPoint = (Random.value > 0.5f) ? spawnPoint1 : spawnPoint2;

            // Instancia o inimigo no ponto de spawn escolhido e adiciona à lista de inimigos ativos
            GameObject newEnemy = Instantiate(enemyPrefab, chosenSpawnPoint.position, chosenSpawnPoint.rotation);
            activeEnemies.Add(newEnemy);

            // Adiciona um callback para quando o inimigo for destruído
            newEnemy.GetComponent<Enemy>().OnDestroyed += () =>
            {
                activeEnemies.Remove(newEnemy);
                enemyCounterUI.IncrementCounter(); // Atualiza o contador de inimigos derrotados
            };

            // Espera um pequeno intervalo antes de spawnar o próximo inimigo
            yield return new WaitForSeconds(0.5f);
        }

        currentRound++;
    }
}
