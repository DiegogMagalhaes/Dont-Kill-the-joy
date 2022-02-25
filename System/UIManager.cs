using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject BlackScreen;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject GameOver;

    [SerializeField] private GameManager gm;
    [SerializeField] private GameObject joy;

    //Eneable and Desable
    public void SwitchBlackScreen(bool on)
    {
        BlackScreen.SetActive(on);
    }

    public void SwitchMenu()
    {
        Menu.SetActive(!Menu.activeSelf);
    }

    public void SwitchGameOver()
    {
        GameOver.SetActive(!Menu.activeSelf);
    }

    //Actions
    public void ResetAct()
    {
        SwitchGameOver();
        joy.SetActive(true);
        gm.ResetGame();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

}
