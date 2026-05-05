using Application.Animals;
using Application.World;
using Domain.Animals;
using Domain.Common;
using Domain.Herd;
using Domain.Score;
using NUnit.Framework;

public class DeliveryServiceTests
{
    [Test]
    public void TryDeliverAnimals_DeliversAnimal_WhenAnimalIsInsideYard()
    {
        var herd = new HerdService(new HerdSettings(5));
        var score = new ScoreService();

        var world = new GameplayWorld(
            new GameBounds(GameVector2.Zero, new GameVector2(10f, 10f)),
            new GameBounds(new GameVector2(-2f, 0f), new GameVector2(5f, 5f)),
            new GameBounds(new GameVector2(4f, 0f), new GameVector2(2f, 4f)));

        var animal = new AnimalModel(
            new AnimalId(1),
            new GameVector2(4f, 0f));

        herd.TryAdd(animal);

        var service = new AnimalDeliveryService(
            herd,
            score,
            world);

        bool deliveredEventRaised = false;
        service.Delivered += _ => deliveredEventRaised = true;

        service.TryDeliverAnimals();

        Assert.IsFalse(herd.Contains(animal));
        Assert.AreEqual(1, score.Value);
        Assert.AreEqual(AnimalStatus.Delivered, animal.Status);
        Assert.IsTrue(deliveredEventRaised);
    }
}
