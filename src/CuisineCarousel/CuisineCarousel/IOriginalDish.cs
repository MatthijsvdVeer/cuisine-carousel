using CuisineCarousel.Models;

namespace CuisineCarousel;

public interface IOriginalDish
{
    public Task<OriginalDish> GetById(Guid id);

    public Task<IEnumerable<OriginalDish>> GetAll();

    Task Create(OriginalDish originalDish);
}
