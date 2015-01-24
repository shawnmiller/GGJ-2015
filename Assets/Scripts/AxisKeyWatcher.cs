
public class AxisKeyWatcher
{
    private const float THRESHOLD = 0.5f;

    private float previous;
    private float current;

    public AxisKeyWatcher()
    {
        previous = current = 0f;
    }

    public void Update(float value)
    {
        previous = current;
        current = value;
    }

    public bool Up()
    {
        return previous > THRESHOLD && current < THRESHOLD;
    }

    public bool Down()
    {
        return previous < THRESHOLD && current > THRESHOLD;
    }

    public bool Pressed()
    {
        return current > THRESHOLD;
    }

    public bool Released()
    {
        return current < THRESHOLD;
    }
}