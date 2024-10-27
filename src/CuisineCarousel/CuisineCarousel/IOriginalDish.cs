using CuisineCarousel.Models;

namespace CuisineCarousel;

public interface IOriginalDish
{
    public OriginalDish GetById(string id);

    public IEnumerable<OriginalDish> GetAll();
}
