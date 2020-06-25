using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scene_1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OptionsMenu()
    {
        Time.timeScale = 0;
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); Time.timeScale = 1;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void StopFreeze()
    {
        CarController.FindObjectOfType<CarController>().isStun = false;
    }
    public void firstLevel()
    {
        SceneManager.LoadScene("Scene_1");
    }
    public void secondLevel()
    {
        SceneManager.LoadScene("Scene_2");
    }
    public void thirdLevel()
    {
        SceneManager.LoadScene("Scene_3");
    }
}
