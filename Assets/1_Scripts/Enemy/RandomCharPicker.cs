using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Xezebo.Enemy
{
    public class RandomCharPicker : MonoBehaviour
    {
        [Button]
        void ChangeModel()
        {
            // TODO: do it in one loop
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }

            int rnd = Random.Range(0, transform.childCount - 1);
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