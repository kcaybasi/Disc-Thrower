using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    private int currentSceneIndex;
    private int sceneToContinue;

    private void Awake()
    {
        DontDestroyOnLoad(this.transform.gameObject);

    }
    void Start()
    {

        ContinueGame();

    }
    
    private void ContinueGame()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");

        if (sceneToContinue != 0)
        {
            SceneManager.LoadScene(sceneToContinue);
            sceneToContinue = 0;

        }
        else
        {
            return;
        }
    }


    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("SavedScene", currentSceneIndex);

        }
        else
        {
            return;
        }
    }
}
