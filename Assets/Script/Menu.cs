using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject fadeInPanel;
    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeIn(sceneName));
    }
    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator  FadeIn(string sceneName)
    {
        fadeInPanel.SetActive(true);
        yield return new WaitForSeconds(0.40f);
        SceneManager.LoadScene(sceneName);
    }
}
