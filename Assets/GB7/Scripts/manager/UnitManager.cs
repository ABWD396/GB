using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{

    [SerializeField]
    private UnitMain unitMain;

    [SerializeField]
    private GameManagerScript gameManagerScript;

    [SerializeField]
    private Image mask;

    [SerializeField]
    private AudioSource unitAudio;

    [SerializeField]
    private Image timeImage;

    [SerializeField]
    private Text costText;

    private bool isManagerFree = true;

    void Awake()
    {
        InitManager();
    }

    public void CreateUnit()
    {
        if (isManagerFree)
        {
            if (gameManagerScript.IsPriceEnough(unitMain.price))
            {
                gameManagerScript.AddLog(String.Format("Не хватает ресурсов для рекрутирования {0}", unitMain.GetName()));
                return;
            }

            isManagerFree = false;
            if (unitAudio)
            {
                unitAudio.Play();
            }
            gameManagerScript.AddLog(String.Format("Юнит {0} рекрутируется", unitMain.GetName()));
            gameManagerScript.AddCallbackTimers(
                unitMain.timer,
                delegate (float timer) { CreateUnitCallback(timer); }
            );
            gameManagerScript.RemoveResource(unitMain.priceType, unitMain.price);
        }
    }

    public void CreateUnitCallback(float timer)
    {
        if (timer <= 0)
        {
            isManagerFree = true;
            AddUnit();
        }

        UpdateMaskState(timer);
    }

    private void UpdateMaskState(float timer)
    {
        mask.fillAmount = timer / unitMain.timer;
    }

    protected void AddUnit(byte count = 1)
    {
        gameManagerScript.AddLog(String.Format("Юнит {0} рекрутирован", unitMain.GetName()));
        gameManagerScript.AddResource(unitMain.resourceType, count);
    }

    public void InitManager()
    {
        isManagerFree = true;
        mask.fillAmount = 0;

        unitMain.UpdateValues();
        costText.text = unitMain.price.ToString();
        float cycleLength = gameManagerScript.cycleManager.GetCycleLength();
        if (unitMain.timer > cycleLength)
        {
            timeImage.color = new Color(1, 0.4803739f, 0);
            timeImage.fillAmount = (unitMain.timer - cycleLength) / cycleLength;
        }
        else
        {
            timeImage.fillAmount = unitMain.timer / cycleLength;
        }
    }
}