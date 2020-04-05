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
using UnityEngine.Audio;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header ("Other")]
    public AudioMixer audioMixer;
    public Animator anim;
    public GameObject transition;

    int index;

    void Start()
    {
        GameObject.FindGameObjectWithTag("Transition").SetActive(true);
        StartCoroutine(StartTransition());
    }

    public void Level01()
    {
        index = 1;
        StartCoroutine(EndTransition());
    }

    public void Level02()
    {
        index = 2;
        StartCoroutine(EndTransition());
    }

    public void Level03()
    {
        index = 3;
        StartCoroutine(EndTransition());
    }

    public void Level04()
    {
        index = 4;
        StartCoroutine(EndTransition());
    }

    public void Level05()
    {
        index = 5;
        StartCoroutine(EndTransition());
    }

    public void Level06()
    {
        index = 6;
        StartCoroutine(EndTransition());
    }

    public void Level07()
    {
        index = 7;
        StartCoroutine(EndTransition());
    }

    public void Level08()
    {
        index = 8;
        StartCoroutine(EndTransition());
    }

    public void Level09()
    {
        index = 9;
        StartCoroutine(EndTransition());
    }

    public void Level10()
    {
        index = 10;
        StartCoroutine(EndTransition());
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        FindObjectOfType<AudioManager>().Play("Click");
    }

    public void TestSound()
    {
        FindObjectOfType<AudioManager>().Play("GunShot");
    }

    public void Click()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    } 

    IEnumerator EndTransition()
    {
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
