using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public const string PlayerMeshKey = "playerMesh";

    private void Start()
    {
        Cursor.lockState  = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            OnGameStart();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnGameStart()
    {
        int playerMeshIndex = FindObjectOfType<PlayerSelector>().ActiveChildIndex;
        PlayerPrefs.SetInt(PlayerMeshKey, playerMeshIndex);
        SceneManager.LoadScene("Tutorial");
    }
}
