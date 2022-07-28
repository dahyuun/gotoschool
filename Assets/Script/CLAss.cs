using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CLAss : MonoBehaviour
{
    void Start()
    {
        Invoke("sceneChange", 1);
    }

    void sceneChange()
    {
        SceneManager.LoadScene("End");
    }
}
