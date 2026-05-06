using Domain.Common;
using Domain.Movement;
using NUnit.Framework;

public class MovementServiceTests
{
    [Test]
    public void MoveTowards_MovesTowardsTarget()
    {
        var service = new MovementService();

        GameVector2 result = service.MoveTowards(
            new GameVector2(0f, 0f),
            new GameVector2(10f, 0f),
            new MovementSettings(5f, 0.01f),
            1f,
            out bool reached);

        Assert.AreEqual(5f, result.X, 0.001f);
        Assert.AreEqual(0f, result.Y, 0.001f);
        Assert.IsFalse(reached);
    }

    [Test]
    public void MoveTowards_ReachesTarget_WhenCloseEnough()
    {
        var service = new MovementService();

        GameVector2 result = service.MoveTowards(
            new GameVector2(0f, 0f),
            new GameVector2(1f, 0f),
            new MovementSettings(5f, 0.01f),
            1f,
            out bool reached);

        Assert.AreEqual(1f, result.X, 0.001f);
        Assert.IsTrue(reached);
    }
}
