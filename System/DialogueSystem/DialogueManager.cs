using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Todo esse codigo foi retirado do tutorial "How to make a Dialogue System in Unity" by Brackeys channel.

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    [SerializeField] private Text DialogueText;
    private UIManager UIManager;
     private DialogueTrigger Trigger;

    private void Awake()
    {
        Trigger = GameObject.Find("TextManager").GetComponent<DialogueTrigger>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentance();
    }

    public void DisplayNextSentance()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentece(sentence));
    }

    IEnumerator TypeSentece(string sentence)
    {
        DialogueText.text = "";

        foreach(char character in sentence.ToCharArray())
        {
            DialogueText.text += "" + character;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        DialogueText.text = "";
        Trigger.GetComponent<DialogueTrigger>().EndTexting();
    }

}
