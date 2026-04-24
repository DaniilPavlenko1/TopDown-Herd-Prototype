using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "HeroConfig",
        menuName = "Configs/Hero Config")]
    public class HeroConfig : ScriptableObject
    {
        [field: SerializeField, Min(0f)]
        public float MoveSpeed { get; private set; } = 5f;
    }
}