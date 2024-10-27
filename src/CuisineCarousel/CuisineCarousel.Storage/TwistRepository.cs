namespace CuisineCarousel.Storage;

internal sealed class TwistRepository : ITwist
{
    private static readonly List<Twist> Twists =
    [
        new("1", "5 Ingredients Or Less", "The dish must contain 5 ingredients or less."),
        new("2", "Deep Fried", "The dish must be deep fried."),
        new("3", "Served In A Wicker Basket", "The dish must be served in a wicker basket.")
    ];
    
    public Twist GetById(string id)
    {
        return Twists.Single(twist => twist.Id == id);
    }

    public IEnumerable<Twist> GetAll()
    {
        return Twists;
    }
}
