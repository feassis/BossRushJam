using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private string firstSceneName;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button levelSelectButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        newGameButton.onClick.AddListener(StartGame);
        continueButton.onClick.AddListener(ContinueGame);
        levelSelectButton.onClick.AddListener(LevelSelect);
        quitButton.onClick.AddListener(QuitGame);
    }

    

    public void StartGame() //Responsible For Game Start
    {
        SceneManager.LoadScene(firstSceneName);
    }

    private void ContinueGame()
    {
        PartExchangeMenu.OpenPartExchangeMenu(MechaTorso.ChainedGhost, new List<MechaArms> { MechaArms.ChainGrappled,
        MechaArms.FlameTorchBeacon, MechaArms.PinkAcidLazer}, MechaLegs.OverHeat);
    }

    private void LevelSelect()
    {

    }

    public void QuitGame() //Responsible For Game Quit
    {
        Application.Quit(); //Quits The Game
    }
    
     
}
