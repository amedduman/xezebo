using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModelSwithcher : MonoBehaviour
{
    void Start()
    {
        List<SkinnedMeshRenderer> chars = new List<SkinnedMeshRenderer>();
        for (int i = 0; i < transform.childCount; i++)
        {
            SkinnedMeshRenderer sm = transform.GetChild(i).GetComponent<SkinnedMeshRenderer>();
            if(sm != null)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                chars.Add(sm);
            }
        }

        chars[Random.Range(0, chars.Count - 1)].gameObject.SetActive(true);
    }
}



