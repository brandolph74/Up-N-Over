using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition_Save : MonoBehaviour
{
    [SerializeField] Animator transition;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endTransition()
    {
        StartCoroutine(fadeOut());
    }

    IEnumerator fadeOut()
    {
        transition.SetTrigger("startFade");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");
    }
    
}
