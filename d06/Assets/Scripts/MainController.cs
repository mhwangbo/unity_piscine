using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    // Detection Level Control
    [HideInInspector] public float detectionLevel;
    public UIController uiController;
    [HideInInspector] public bool detected;
    [HideInInspector] public bool run;
    [HideInInspector] public bool cctvDetected;
    private bool warning;

    [HideInInspector] public bool cardKey;
    [HideInInspector] public bool document;

    // GameOver
    [HideInInspector] public bool gameOver;

    // Background Music
    public AudioSource normalAudio;
    public AudioSource panicAudio;
    public AudioSource warningAudio;

    // Interaction
    [HideInInspector] public int objectVisible = -1;
    public ObjectController objectController;

    private void Update()
    {
        if (!gameOver)
            CalculateDetectionLevel();
        if (detectionLevel >= 1.0f)
            GameStatus("Mission Has Failed\nReinitializing........");
        if (document)
            GameStatus("Mission Completed\nReinitializing........");
        SetInstruction();
        if (objectVisible > 0 && Input.GetKeyDown("e"))
        {
            objectController.TakeAction(objectVisible);
        }
    }

    private void SetInstruction()
    {
        string instruction;
        switch (objectVisible)
        {
            case 1:
                instruction = "Press E to activate";
                break;
            case 2:
                instruction = "Press E to pickup";
                break;
            case 3:
                if (cardKey)
                    instruction = "Press E to use a cardkey";
                else
                    instruction = "Need to find a cardkey";
                break;
            case 4:
                instruction = "Press E to pickup";
                break;
            default:
                instruction = "";
                break;
        }
        uiController.SetInstruction(instruction);
    }

    private void CalculateDetectionLevel()
    {
        float detectionCalc = -0.001f;

        if (cctvDetected)
            detectionCalc += 0.01f;
        if (detected)
            detectionCalc += 0.005f;
        if (run)
            detectionCalc += 0.003f;
        detectionLevel += detectionCalc;
        if (detectionLevel < 0f || detectionLevel > 1f)
            detectionLevel = (detectionLevel < 0f ? 0f : 1f);
        uiController.SetDetectionBar(detectionLevel);
        if (detectionLevel >= 0.75 && !warning)
            BlinkText(true);
        if (detectionLevel < 0.75 && warning)
            BlinkText(false);
    }

    private void BlinkText(bool trueOrFalse)
    {
        uiController.StartBlinking(trueOrFalse);
        warning = trueOrFalse;
        if (warning)
        {
            normalAudio.Stop();
            panicAudio.Play();
            warningAudio.Play();
        }
        else
        {
            panicAudio.Stop();
            normalAudio.Play();
            warningAudio.Stop();
        }
    }

    private void GameStatus(string str)
    {
        gameOver = true;
        uiController.GameMessage(str);
        uiController.StartBlinking(false);
        if (warning)
            BlinkText(false);
    }
}
