using System.Linq;
using Application.Animals;
using Application.Common;
using Application.World;
using Domain.Animals;
using Domain.Common;
using Domain.Herd;
using Domain.Score;
using NUnit.Framework;

public class DeliveryEventFlowTests
{
    [Test]
    public void DeliveryService_PublishesDeliveredEvent_ScoreHandlerAddsScore_DespawnHandlerRemovesAnimal()
    {
        var eventBus = new EventBus();
        var scoreService = new ScoreService();
        var herdService = new HerdService(new HerdSettings(5));

        var world = new GameplayWorld(
            new GameBounds(GameVector2.Zero, new GameVector2(10f, 10f)),
            new GameBounds(new GameVector2(-2f, 0f), new GameVector2(5f, 5f)),
            new GameBounds(new GameVector2(4f, 0f), new GameVector2(2f, 4f)));

        var spawnService = new AnimalSpawnService(
            world,
            eventBus,
            new SystemRandomService(seed: 123));

        var deliveryService = new AnimalDeliveryService(
            herdService,
            world,
            eventBus);

        var scoreHandler = new AnimalDeliveredScoreHandler(eventBus, scoreService);
        var despawnHandler = new AnimalDeliveredDespawnHandler(eventBus, spawnService);

        AnimalModel animal = spawnService.Spawn();
        animal.SetPosition(new GameVector2(4f, 0f));
        herdService.TryAdd(animal);

        deliveryService.TryDeliverAnimals();

        Assert.AreEqual(1, scoreService.Value);
        Assert.IsFalse(herdService.Contains(animal));
        Assert.IsFalse(spawnService.Animals.Contains(animal));
        Assert.AreEqual(AnimalStatus.Delivered, animal.Status);

        despawnHandler.Dispose();
        scoreHandler.Dispose();
    }
}
