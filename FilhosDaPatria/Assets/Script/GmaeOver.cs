using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GmaeOver : MonoBehaviour
{
    public void TentarNovamente()
    {
        SceneManager.LoadScene("Level");
    }
    public void SairJogo()
    { 
        Application.Quit();
    }
}
