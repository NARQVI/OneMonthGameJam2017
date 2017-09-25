using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public GameObject textbox;
    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public PlayerController player;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

    }

    // Update is called once per frame
    void Update()
    {

        theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.Space))

        {
            currentLine += 1;

        }

        if(currentLine > endAtLine)
        {
            textbox.SetActive(false);
        }
            
            }


}
