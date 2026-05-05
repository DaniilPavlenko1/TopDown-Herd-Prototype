using System.Collections.Generic;
using Application.Animals;
using Domain.Animals;
using Domain.Animals.States;
using Domain.Common;
using Domain.Herd;
using NUnit.Framework;

public class CollectionServiceTests
{
    private sealed class DummyStateFactory : IAnimalStateFactory
    {
        public IAnimalState CreatePatrolState() => new DummyState();
        public IAnimalState CreateFollowState() => new DummyState();
    }

    private sealed class DummyState : IAnimalState
    {
        public void Enter(AnimalModel animal) { }
        public void Tick(AnimalModel animal, float deltaTime) { }
        public void Exit(AnimalModel animal) { }
    }

    [Test]
    public void TryCollectNearbyAnimals_AddsAnimalToHerd_WhenInsideCollectRadius()
    {
        var herd = new HerdService(new HerdSettings(5));

        var settings = new AnimalSettings(
            moveSpeed: 3f,
            stopDistance: 0.05f,
            followDistance: 0.7f,
            collectRadius: 2f,
            patrolRadius: 3f,
            patrolPointReachDistance: 0.15f);

        var service = new AnimalCollectionService(
            herd,
            settings,
            new DummyStateFactory());

        var animal = new AnimalModel(
            new AnimalId(1),
            new GameVector2(1f, 0f));

        service.TryCollectNearbyAnimals(
            GameVector2.Zero,
            new List<AnimalModel> { animal });

        Assert.AreEqual(1, herd.Count);
        Assert.AreEqual(AnimalStatus.Follow, animal.Status);
    }

    [Test]
    public void TryCollectNearbyAnimals_DoesNotCollect_WhenOutsideRadius()
    {
        var herd = new HerdService(new HerdSettings(5));

        var settings = new AnimalSettings(
            3f,
            0.05f,
            0.7f,
            1f,
            3f,
            0.15f);

        var service = new AnimalCollectionService(
            herd,
            settings,
            new DummyStateFactory());

        var animal = new AnimalModel(
            new AnimalId(1),
            new GameVector2(5f, 0f));

        service.TryCollectNearbyAnimals(
            GameVector2.Zero,
            new List<AnimalModel> { animal });

        Assert.AreEqual(0, herd.Count);
    }
}
