using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject _panel;
    void Start()
    {
        _panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            _panel.SetActive(true);
        }
        else
        {
            _panel.SetActive(false);
        }
    }
}
