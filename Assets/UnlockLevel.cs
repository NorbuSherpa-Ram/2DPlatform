using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockLevel : MonoBehaviour
{
    public string sceneName;
    public Image  lockImg;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
     //   lockImg = GetComponentInParent <Image>();
    }
    private void Update()
    {
        if(PlayerPrefs.GetInt (sceneName ,0)==1)
        {
            button.interactable = true;
            lockImg.enabled = false;
        }
    }
}
