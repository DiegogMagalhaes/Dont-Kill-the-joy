using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("DialogBox Elements")]
    [SerializeField] private GameObject Sentence;

    [Header("Dialogues")]
    [SerializeField] private Dialogue[] dialogues;

    [Header("OtherManagers")]
    [SerializeField] private GameManager GameManager;

    [SerializeField] GameObject animationStart;
    private bool canText = false;
    private DialogueManager dm;

    //gambiarra pra cutscene
    private int CurrentAct;
    private int numberofclick = 0;
    [SerializeField] private GameObject cutsceneImage;

    private void Awake()
    {
        dm = GameObject.Find("Dialogue").GetComponent<DialogueManager>();
        Debug.Log(dm);
    }

    private void Update()
    {
        if (canText)
        {
            if (Input.anyKeyDown)
            {
                dm.DisplayNextSentance();
                //gambiara
                if (CurrentAct == 1 && cutsceneImage.activeSelf)
                {
                    numberofclick++;
                    if (numberofclick >= 2) cutsceneImage.SetActive(false); 
                }

            }
        }

        if (Input.GetKey(KeyCode.T)) StartTexting(1);
    }

    public void StartTexting(int act)
    {
        canText = true;
        CurrentAct = act;

        Sentence.SetActive(true);
        Debug.Log(dm);
        Debug.Log(dialogues[act - 1]);
        dm.StartDialogue(dialogues[act-1]);
    }

    public void EndTexting()
    {
        canText = false;
        Sentence.SetActive(false);
        if(!GameManager.IsOver()) animationStart.SetActive(true);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3.2f);
        GameManager.StartGame();
    }

}
