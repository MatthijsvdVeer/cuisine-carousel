namespace CuisineCarousel;

public interface ITwist
{
    public Twist GetById(string id);

    public IEnumerable<Twist> GetAll();
}
