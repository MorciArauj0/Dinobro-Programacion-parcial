using UnityEngine;
using System.Collections;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject buttonE;
    [SerializeField] private bool esVendedor = false;
    [SerializeField] private int monedasRequeridas = 12;
    [SerializeField] private GameObject key;
    [SerializeField, TextArea(4, 6)] private string[] dialogueSinMonedas;
    [SerializeField, TextArea(4, 6)] private string[] dialogueConMonedas;



    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int currentLineIndex;

    private void Start()
    {
        if(buttonE != null)
        {
            buttonE.SetActive(false);
        }

        if (esVendedor)
            dialogueLines = new string[0];
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueLines != null &&
                    dialogueLines.Length > 0 &&
                    currentLineIndex < dialogueLines.Length &&
                    dialogueText.text == dialogueLines[currentLineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                if (dialogueLines != null && currentLineIndex < dialogueLines.Length)
                    dialogueText.text = dialogueLines[currentLineIndex];
            }
        }
    }
    
    private void StartDialogue()
    {
        if (!didDialogueStart)
        {
            didDialogueStart = true;
            currentLineIndex = 0;
            dialoguePanel.SetActive(true);
            if (buttonE != null)
            {
                buttonE.SetActive(false);
            }

            if (esVendedor)
            {
                int monedas = ControladorCoins.instance != null ?
                          ControladorCoins.instance.GetCantidadCoins() : 0;

                if (monedas >= monedasRequeridas)
                    dialogueLines = dialogueConMonedas;
                else
                    dialogueLines = dialogueSinMonedas;
            }

            StartCoroutine(ShowLine());
        }
    }

    private void NextDialogueLine()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);

            if (esVendedor)
            {
                int monedas = ControladorCoins.instance != null ?
                              ControladorCoins.instance.GetCantidadCoins() : 0;

                if (monedas >= monedasRequeridas)
                {
                    ControladorCoins.instance.SumarCoins(-monedasRequeridas);
                    if (key != null) key.SetActive(true);
                }
                else
                {
                    GameManager gameManager = FindAnyObjectByType<GameManager>();
                    if (gameManager != null)
                        gameManager.MostrarPanelDerrota();
                }
            }
        }   
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[currentLineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        isPlayerInRange = true;

        if (buttonE != null)
            buttonE.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isPlayerInRange = false;

        if (buttonE != null)
            buttonE.SetActive(false);
    }
}
