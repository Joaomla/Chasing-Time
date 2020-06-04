using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [SerializeField] int textBoxInt = 0;
    [SerializeField] Text[] texts;

    LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        foreach (Text text in texts)
        {
            text.enabled = false;
            Debug.Log(text.name);
        }

        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) { ShowText(); }
    }

    private void ShowText()
    {
        if (textBoxInt >= texts.Length)
        {
            levelManager.LoadNextLevel();

        }
        else
        {
            texts[textBoxInt].enabled = true;

            textBoxInt++;
        }
    }


}
