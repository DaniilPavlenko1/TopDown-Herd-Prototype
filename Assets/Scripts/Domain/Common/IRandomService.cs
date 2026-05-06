namespace Domain.Common
{
    public interface IRandomService
    {
        float Range01();
        float Range(float min, float max);
    }
}
