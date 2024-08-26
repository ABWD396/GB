using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Singleton]
public class GameManagerScript : MonoBehaviour
{

    [SerializeField]
    private Text logText;

    [SerializeField]
    public CycleManagerScript cycleManager;
    public TimeManager timeManager;
    public ResourceManager resourceManager;

    public UnitManager[] unitManagers;
    public AudioSource backgroundAudio;

    public GameObject finalResult;

    void Update()
    {
        if (timeManager.GetIsGamePaused())
        {
            return;
        }

        timeManager.Handle(Time.deltaTime);
        cycleManager.Handle(timeManager.GetMainTimer());
    }

    public void AddCallbackTimers(float timer, Action<float> callback)
    {
        timeManager.AddCallbackTimers(timer, callback);
    }

    public void AddResource(ResourceType resourceType, int count)
    {
        resourceManager.AddResource(resourceType, count);
    }
    public void RemoveResource(ResourceType resourceType, int count)
    {
        resourceManager.RemoveResource(resourceType, count);
    }

    public void AddLog(string text)
    {
        logText.text += String.Format("[{0}]: {1}\n", timeManager.FormatTime(cycleManager.GetCurrentCycle(), timeManager.GetMainTimer()), text);
    }

    public bool IsPriceEnough(int price)
    {
        return resourceManager.GetResource(ResourceType.Wheat) < price;
    }

    public void StartGame()
    {
        timeManager.PauseGame();

        timeManager.InitManager();
        cycleManager.InitManager();
        resourceManager.InitManager(true);

        foreach (UnitManager unitManager in unitManagers)
        {
            unitManager.InitManager();
        }

        backgroundAudio.Play();

        timeManager.ResumeGame(true);
    }

    public void FinishGame(bool result)
    {
        Text finalResultText = finalResult.GetComponentInChildren<Text>();

        if (result)
        {
            finalResultText.text = "Победа!\n Деревня спасена!";
        }
        else
        {
            finalResultText.text = "Поражение!\n Деревня разорена!";
        }

        finalResult.SetActive(true);
    }

    public void DoExitGame()
    {
        Application.Quit();
    }


}