using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneSequencer : MonoBehaviour
{
    [SerializeField] private Image cutsceneImage;
    [SerializeField] private TextMeshProUGUI cutsceneText;
    [SerializeField] private string nextSceneName;
    [SerializeField] private List<CutsceneSequenceConfig> configs;

    private List<CutsceneSequence> sequences = new List<CutsceneSequence>();

    private int index = 0;

    [Serializable]
    private class CutsceneSequenceConfig
    {
        public Sprite CutSceneImage;
        public List<string> texts = new List<string>();
    }

    private CutsceneSequencerInput cutsceneInput;
    private InputAction nextAction;

    private class CutsceneSequence
    {
        public Sprite CutSceneImage;
        public List<string> texts = new List<string>();

        private int index = 0;

        public CutsceneSequence(CutsceneSequenceConfig config)
        {
            CutSceneImage = config.CutSceneImage;
            texts = config.texts;
        }

        public bool HasEnded() => index >= texts.Count;

        public string GetCurrentText() => texts[index];

        public void GoToNextText() => index++;
    }

    private void Awake()
    {
        foreach(var config in configs) 
        {
            sequences.Add(new CutsceneSequence(config));
        }

        UpdateCutscene();

        cutsceneInput = new CutsceneSequencerInput();
        cutsceneInput.Enable();

        nextAction = cutsceneInput.FindAction("Next");

        nextAction.performed += OnNextActionClicked;
    }

    private void OnNextActionClicked(InputAction.CallbackContext context)
    {
        GoToNextStage();
    }

    private void GoToNextStage()
    {
        sequences[index].GoToNextText();

        if (sequences[index].HasEnded())
        {
            index++;
        }

        if (index > sequences.Count - 1)
        {
            GoToNextScene();
            return;
        }

        UpdateCutscene();
    }

    private void GoToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnEnable()
    {
        cutsceneInput.Enable();
    }

    private void OnDisable()
    {
        cutsceneInput.Disable();
    }

    private void UpdateCutscene()
    {
        cutsceneImage.sprite = sequences[index].CutSceneImage;
        cutsceneText.text = sequences[index].GetCurrentText();
    }
}
