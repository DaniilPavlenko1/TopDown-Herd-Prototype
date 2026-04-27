using Animals;
using Configs;
using UnityEngine;

namespace Hero
{
    public class AnimalCollector : MonoBehaviour
    {
        [SerializeField] private AnimalConfig animalConfig;
        [SerializeField] private LayerMask animalLayerMask;

        private readonly Collider2D[] _results = new Collider2D[16];

        private void Update()
        {
#pragma warning disable CS0618
            int count = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                animalConfig.CollectRadius,
                _results,
                animalLayerMask);
#pragma warning restore CS0618

            for (int i = 0; i < count; i++)
            {
                if (_results[i].TryGetComponent(out AnimalController animal))
                {
                    animal.TryCollect();
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (animalConfig == null)
                return;

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, animalConfig.CollectRadius);
        }
    }
}