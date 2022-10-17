using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Button_Actions : MonoBehaviour
{
    public void playButton()
    {
        try 
        {
            SceneManager.LoadScene("SampleScene");
            Debug.Log("Play Button Pressed");
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }   
    }

    public void exitButton()
    {
        try
        {
            //exit unity
            Application.Quit();
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
}
