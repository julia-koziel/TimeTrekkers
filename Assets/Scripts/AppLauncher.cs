using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
 
 public class AppLauncher : MonoBehaviour
 {
 
     // Use this for initialization
     void Start()
     {
         if(PlayerPrefs.GetInt("FirstLaunch") == 0)
         {
             //First launch
             PlayerPrefs.SetInt("FirstLaunch", 1);
             SceneManager.LoadScene(0);
         }
         else
         {
             //Load scene_02 if its not the first launch
             SceneManager.LoadScene(1);
         }
     }
 
 }