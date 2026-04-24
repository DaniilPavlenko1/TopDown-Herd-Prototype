using Configs;
using Herd;
using UnityEngine;

namespace Animals.States
{
    public class FollowHeroAnimalState : IAnimalState
    {
        private readonly Transform _animalTransform;
        private readonly Transform _heroTransform;
        private readonly AnimalMover _mover;
        private readonly AnimalConfig _config;
        private readonly IHerdService _herdService;
        private readonly AnimalController _animal;

        public FollowHeroAnimalState(
            AnimalController animal,
            Transform animalTransform,
            Transform heroTransform,
            AnimalMover mover,
            AnimalConfig config,
            IHerdService herdService)
        {
            _animal = animal;
            _animalTransform = animalTransform;
            _heroTransform = heroTransform;
            _mover = mover;
            _config = config;
            _herdService = herdService;
        }

        public void Enter()
        {
        }

        public void Tick(float deltaTime)
        {
            Vector3 target = GetFollowTarget();

            float distance = Vector3.Distance(_animalTransform.position, target);

            if (distance <= _config.FollowDistance)
                return;

            _mover.MoveTo(target, deltaTime);
        }

        public void Exit()
        {
        }

        private Vector3 GetFollowTarget()
        {
            var animals = _herdService.GetAnimals();

            int index = -1;

            for (int i = 0; i < animals.Count; i++)
            {
                if (animals[i] == _animal)
                {
                    index = i;
                    break;
                }
            }

            if (index <= 0)
                return _heroTransform.position;

            return animals[index - 1].transform.position;
        }
    }
}