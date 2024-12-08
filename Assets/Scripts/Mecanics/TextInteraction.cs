using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextInteraction : MonoBehaviour
{
    private bool isPlayerinRange = false;
    [SerializeField] private GameObject TextPanel;
    [SerializeField] private TMP_Text text;
    [SerializeField, TextArea (4,6)] private string[] textLines;
    public GameObject Portal;
    private bool didDialogueStart;
    private int lineIndex;
    public GameObject weapon;


    private void Start()
    {
        Portal.SetActive (false);
        TextPanel.SetActive(false);

    }


    void Update()
    {
        if (isPlayerinRange && Input.GetKeyDown("e")) 
        {
            if(!didDialogueStart)
            {
                StartDialogue();
            }
            else if(text.text == textLines[lineIndex])
            {

                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                text.text = textLines[lineIndex];
            }
 
        }

    }


    private void StartDialogue()
    {
        didDialogueStart = true;
        TextPanel.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
        weapon.SetActive(false);

    }
    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < textLines.Length) 
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            TextPanel.SetActive(false);
            Time.timeScale = 1f;
            Portal.SetActive(true);
            weapon.SetActive(true);
        }


    }

    private IEnumerator ShowLine()
    {

        text.text = string.Empty;

        foreach(char ch in textLines[lineIndex]) 
        {

            text.text += ch;
            yield return new WaitForSecondsRealtime(0.05f);

        }
    


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            isPlayerinRange = true;
            Debug.Log("Pulsar Para Interactuar");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            isPlayerinRange = false;
            Debug.Log("No se puede Interactuar");
        }
    }
}
