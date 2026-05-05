using Domain.Score;
using NUnit.Framework;

public class ScoreServiceTests
{
    [Test]
    public void Add_IncreasesScore_WhenAmountIsPositive()
    {
        var score = new ScoreService();

        score.Add(3);

        Assert.AreEqual(3, score.Value);
    }

    [Test]
    public void Add_DoesNothing_WhenAmountIsZeroOrNegative()
    {
        var score = new ScoreService();

        score.Add(0);
        score.Add(-5);

        Assert.AreEqual(0, score.Value);
    }

    [Test]
    public void Changed_IsRaised_WhenScoreChanges()
    {
        var score = new ScoreService();
        int receivedValue = -1;

        score.Changed += value => receivedValue = value;

        score.Add(2);

        Assert.AreEqual(2, receivedValue);
    }
}
