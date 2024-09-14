using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int currentLevel = 0;
    public Animator ghostAnimator;
    public FinishController finishController;
    public GameObject player;


    public void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        if (ghostAnimator != null)
        {
            ghostAnimator.SetInteger("CurrentLevel", currentLevel);
        }
    }

    public void FinishLevel()
    {
        finishController.Finish();

    }

    public void NextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings <= currentLevel + 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentLevel + 1);
        }
    }

    public void FailLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
