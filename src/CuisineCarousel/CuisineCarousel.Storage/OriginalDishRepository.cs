using CuisineCarousel.Models;

namespace CuisineCarousel.Storage;

internal sealed class OriginalDishRepository : IOriginalDish
{
    private static readonly List<OriginalDish> OriginalDishes =
    [
        new("1", "Spaghetti Carbonara", "A pasta dish made with eggs, cheese, bacon, and black pepper."),
        new("2", "Chicken Tikka Masala", "A dish of roasted chicken chunks in a spicy sauce."),
        new("3", "Beef Wellington", "A beef fillet coated with pâté and duxelles, which is then wrapped in puff pastry.")
    ];
    
    public OriginalDish GetById(string id)
    {
        return OriginalDishes.Single(originalDish => originalDish.Id == id);
    }

    public IEnumerable<OriginalDish> GetAll()
    {
        return OriginalDishes;
    }
}