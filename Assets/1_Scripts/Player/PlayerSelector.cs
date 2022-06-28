using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [SerializeField] AudioSource _clickAudio;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeMeshRight();
            _clickAudio.Play();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeMeshLeft();
            _clickAudio.Play();
        }
    }

    public int ActiveChildIndex = 0;

    public void ChangeMeshRight()
    {
        ChangeMesh(1);
    }

    public void ChangeMeshLeft()
    {
        ChangeMesh(-1);
    }

    void ChangeMesh(int num)
    {
        int nextPlayerMeshIndex = ActiveChildIndex + num;
        nextPlayerMeshIndex = Mathf.Clamp(nextPlayerMeshIndex, 0, transform.childCount - 2); 
        transform.GetChild(ActiveChildIndex).gameObject.SetActive(false);
        transform.GetChild(nextPlayerMeshIndex).gameObject.SetActive(true);
        ActiveChildIndex = nextPlayerMeshIndex;    
    }
}
