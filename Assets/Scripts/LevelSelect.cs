using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelSelect : MonoBehaviour
{
    
    void OnTutorialSelect()
    {
        SceneManager.LoadScene(0);
    }
    void OnAdventureSelect()
    {
        SceneManager.LoadScene(2);
    }
    void OnChallengeSelect()
    {
        SceneManager.LoadScene(3);
    }
}
