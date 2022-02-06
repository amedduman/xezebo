using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class RandomCharPicker : MonoBehaviour
    {
        void Start()
        {
            // TODO: do it in one loop
            
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }

            int rnd = Random.Range(0, transform.childCount);
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == rnd)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
}