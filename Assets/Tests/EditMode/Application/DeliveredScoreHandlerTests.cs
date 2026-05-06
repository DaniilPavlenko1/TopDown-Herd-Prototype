using Application.Animals;
using Application.Animals.Events;
using Application.Common;
using Domain.Animals;
using Domain.Common;
using Domain.Score;
using NUnit.Framework;

public class DeliveredScoreHandlerTests
{
    [Test]
    public void Handler_AddsScore_WhenAnimalDeliveredEventIsPublished()
    {
        var eventBus = new EventBus();
        var scoreService = new ScoreService();
        var handler = new AnimalDeliveredScoreHandler(eventBus, scoreService);

        eventBus.Publish(new AnimalDeliveredEvent(
            new AnimalModel(new AnimalId(1), GameVector2.Zero)));

        Assert.AreEqual(1, scoreService.Value);

        handler.Dispose();
    }
}
