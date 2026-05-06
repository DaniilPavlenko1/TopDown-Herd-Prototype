using Domain.Animals;
using Domain.Common;
using Domain.Herd;
using NUnit.Framework;

public class HerdServiceTests
{
    [Test]
    public void TryAdd_AddsAnimal_WhenHerdIsNotFull()
    {
        var herd = new HerdService(new HerdSettings(5));
        var animal = new AnimalModel(new AnimalId(1), GameVector2.Zero);

        bool result = herd.TryAdd(animal);

        Assert.IsTrue(result);
        Assert.AreEqual(1, herd.Count);
        Assert.IsTrue(herd.Contains(animal));
    }

    [Test]
    public void TryAdd_DoesNotAddAnimal_WhenHerdIsFull()
    {
        var herd = new HerdService(new HerdSettings(1));

        var first = new AnimalModel(new AnimalId(1), GameVector2.Zero);
        var second = new AnimalModel(new AnimalId(2), GameVector2.Zero);

        herd.TryAdd(first);
        bool result = herd.TryAdd(second);

        Assert.IsFalse(result);
        Assert.AreEqual(1, herd.Count);
    }

    [Test]
    public void Remove_RemovesAnimalFromHerd()
    {
        var herd = new HerdService(new HerdSettings(5));
        var animal = new AnimalModel(new AnimalId(1), GameVector2.Zero);

        herd.TryAdd(animal);
        bool removed = herd.Remove(animal);

        Assert.IsTrue(removed);
        Assert.AreEqual(0, herd.Count);
    }
}
