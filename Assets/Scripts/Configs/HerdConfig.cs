using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "HerdConfig",
        menuName = "Configs/Herd Config")]
    public class HerdConfig : ScriptableObject
    {
        [field: SerializeField, Min(1)]
        public int MaxAnimals { get; private set; } = 5;
    }
}