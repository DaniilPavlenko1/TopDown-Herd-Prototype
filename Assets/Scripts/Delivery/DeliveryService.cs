using Animals;
using Herd;
using Score;

namespace Delivery
{
    public class DeliveryService
    {
        private readonly IHerdService _herdService;
        private readonly IScoreService _scoreService;

        public DeliveryService(
            IHerdService herdService,
            IScoreService scoreService)
        {
            _herdService = herdService;
            _scoreService = scoreService;
        }

        public void DeliverAnimal(AnimalController animal)
        {
            if (!_herdService.Contains(animal))
                return;

            _herdService.RemoveAnimal(animal);
            _scoreService.AddScore(1);

            animal.Deliver();
        }
    }
}