//FROM PROJECT ROROPE || REAL GAMES STUDIO
//************************************************
//realgamesss.weebly.com
//gamejolt.com/@Real_Game
//realgamesss.newgrounds.com/
//real-games.itch.io/
//youtube.com/channel/UC_Adg-mo-IPg6uLacuQCZCQ
//************************************************

using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UI : MonoBehaviour
{
    [Header ("GameObjects")]
    public GameObject PauseMenu;
    public GameObject GameOverMenu;
    public GameObject VictoryMenu;
    public GameObject Gun, GrapplingGun;
    public GameObject SpeedBoost, JumpBoost;
    public GameObject transition;

    [Header ("Other")]
    public Animator anim;
    
    int index;
    
    void Start()
    {
        Time.timeScale = 1.0f;
        GameOverMenu.SetActive(false);

        GameObject.FindGameObjectWithTag("Transition").SetActive(true);
        StartCoroutine(StartTransition());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameOverMenu.activeSelf == false && VictoryMenu.activeSelf == false)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0.0f; 
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        index = 0;
        StartCoroutine(EndTransition());
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }

    public void Restart()
    {
        index = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(EndTransition());
    }

    public void NextLevel()
    {
        index = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(EndTransition());
    }

    IEnumerator EndTransition()
    {
        Time.timeScale = 1.0f;
        transition.SetActive(true);
        anim.SetBool("End", true);
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(index);
    }

    IEnumerator StartTransition()
    {
        yield return new WaitForSeconds(1);
        transition.SetActive(false);
    }
}

