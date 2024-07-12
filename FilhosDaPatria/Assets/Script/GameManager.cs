using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver = false;
    static GameManager current;

    // Start is called before the first frame update
    void Awake()
    {
            if(current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }

        current = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(current == null)
        {
            return;
        }
    }

    public static void PlayerDied()
    {
        if(current == null)
        {
            return;
        }
        current.Invoke("RestartScene", 3f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
