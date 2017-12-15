using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void SceneChanger(string name)
    {
        SceneManager.LoadScene(name);
    }
}
