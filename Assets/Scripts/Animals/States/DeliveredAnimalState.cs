using UnityEngine;

namespace Animals.States
{
    public class DeliveredAnimalState : IAnimalState
    {
        private readonly GameObject _animalObject;

        public DeliveredAnimalState(GameObject animalObject)
        {
            _animalObject = animalObject;
        }

        public void Enter()
        {
            _animalObject.SetActive(false);
        }

        public void Tick(float deltaTime)
        {
        }

        public void Exit()
        {
        }
    }
}