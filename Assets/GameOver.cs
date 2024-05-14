using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public void returnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
