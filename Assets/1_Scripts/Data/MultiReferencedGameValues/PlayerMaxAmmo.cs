using UnityEngine;

namespace Xezebo.Data
{
    [CreateAssetMenu(fileName = "PlayerMaxAmmo", menuName = "SO/GameValues", order = 0)]
    public class PlayerMaxAmmo : ScriptableObject
    {
        public int MaxAmmo = 10;
    }
}