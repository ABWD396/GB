using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Singleton]
public class CycleManagerScript : MonoBehaviour
{

    [SerializeField]
    GameManagerScript gameManagerScript;

    [SerializeField]
    private Image cycleImage;

    [SerializeField]
    private Text mainTimerText;

    [SerializeField]
    private Text raiderTimerText;

    private byte cycleLength = 24;

    private byte cycleMultiplier = 4;

    private int currentCycle = 0;
    private int processedCycle = 0;

    [SerializeField]
    private int victoryWheatCount = 250;

    [SerializeField]
    private int raiderNextCycle;

    [SerializeField]
    private float raiderNextCycleModifier;

    [SerializeField]
    private float raiderMultiplier;

    CycleManagerScript()
    {
        InitManager();
    }

    public void InitManager()
    {
        currentCycle = 0;
        processedCycle = 0;
        raiderNextCycle = 2;
        raiderMultiplier = 1.4f;
        raiderNextCycleModifier = 2;
        UpdateRaiderTimer();
    }

    void Awake()
    {

        UpdateRaiderTimer();
    }

    public void Handle(float timerParam)
    {
        float timer = timerParam * cycleMultiplier;

        HandleImageTimer(timer - currentCycle * cycleLength);

        currentCycle = (int)Mathf.Floor(timer / cycleLength);
        if (processedCycle < currentCycle)
        {
            NewCycleHandle();

            processedCycle = currentCycle;
        }
        else
        {
            CurrentCycleHandle();
        }

        mainTimerText.text = gameManagerScript.timeManager.FormatTime(currentCycle, timer - currentCycle * cycleLength);

    }

    protected void HandleImageTimer(float timer)
    {
        float fullTimer = timer * 2;

        cycleImage.fillAmount = (cycleImage.fillClockwise ? (fullTimer / cycleLength) : 1 - (fullTimer / cycleLength))
        - (fullTimer - (fullTimer % cycleLength)) / cycleLength;


        if (cycleImage.fillAmount == 0)
        {
            cycleImage.fillClockwise = true;
        }
        else if (cycleImage.fillAmount == 1)
        {

            cycleImage.fillClockwise = false;
        }
    }

    protected void CurrentCycleHandle()
    {
        if (gameManagerScript.resourceManager.GetResource(ResourceType.Wheat) < 0)
        {
            if (!gameManagerScript.timeManager.GetIsTimerStopped())
            {
                gameManagerScript.timeManager.PauseTimers();
            }
        }
        else
        {
            if (gameManagerScript.timeManager.GetIsTimerStopped())
            {
                gameManagerScript.timeManager.ResumeTimers();
            }
        }
    }

    protected void NewCycleHandle()
    {
        gameManagerScript.AddLog("Начало нового цикла!");

        int[] resources = gameManagerScript.resourceManager.GetResources();

        int wheat = resources[(byte)ResourceType.Wheat];
        int villagers = resources[(byte)ResourceType.Villagers];
        int guards = resources[(byte)ResourceType.Guards];

        int raiders = resources[(byte)ResourceType.Raiders];

        Villagers villager = (Villagers)gameManagerScript.resourceManager.GetUnit(ResourceType.Villagers);
        Guard guard = (Guard)gameManagerScript.resourceManager.GetUnit(ResourceType.Guards);

        gameManagerScript.AddLog(String.Format("До вторжения рейдеров - {0}! Количество: {1}", raiderNextCycle - currentCycle, raiders));

        wheat += Villagers.wheatProduceCount * villagers - (villagers * villager.food + guards * guard.food);
        gameManagerScript.resourceManager.SetResource(ResourceType.Wheat, wheat);

        if (wheat >= victoryWheatCount)
        {
            gameManagerScript.AddLog("Деревня спасена!");
            gameManagerScript.timeManager.PauseGame(true);
            gameManagerScript.FinishGame(true);
        }

        //Battle
        if (currentCycle == raiderNextCycle)
        {
            guards -= raiders;
            raiderNextCycle = (int)Mathf.Ceil(currentCycle + raiderNextCycleModifier);
            raiderNextCycleModifier *= raiderMultiplier;
            raiders = (int)Mathf.Ceil(raiders * raiderMultiplier);
            gameManagerScript.resourceManager.SetResource(ResourceType.Guards, guards);
            gameManagerScript.resourceManager.SetResource(ResourceType.Raiders, raiders);

            if (guards < 0)
            {
                gameManagerScript.AddLog("Деревня разорена!");
                gameManagerScript.timeManager.PauseGame(true);
                gameManagerScript.FinishGame(false);
            }
            else
            {
                gameManagerScript.AddLog("Победа!");
            }
        }

        UpdateRaiderTimer();
    }

    private void UpdateRaiderTimer()
    {
        if (raiderTimerText)
        {
            raiderTimerText.text = String.Format("{0}D", raiderNextCycle - currentCycle);
        }
    }

    public int GetCurrentCycle()
    {
        return currentCycle;
    }

    public float GetCycleLength()
    {
        return cycleLength / (cycleMultiplier * 2);
    }

}