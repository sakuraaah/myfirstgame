using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishMenuBehaviour : MonoBehaviour
{

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
