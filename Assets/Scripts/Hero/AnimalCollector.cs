using Animals;
using Configs;
using UnityEngine;

namespace Hero
{
    public class AnimalCollector : MonoBehaviour
    {
        [SerializeField] private AnimalConfig animalConfig;

        private void Update()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(
                transform.position,
                animalConfig.CollectRadius);

            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent(out AnimalController animal))
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