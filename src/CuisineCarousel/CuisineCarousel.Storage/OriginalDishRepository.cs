using CuisineCarousel.Models;

namespace CuisineCarousel.Storage;

internal sealed class OriginalDishRepository : IOriginalDish
{
    private static readonly List<OriginalDish> OriginalDishes =
    [
        new("1", "Spaghetti Carbonara", "A pasta dish made with eggs, cheese, bacon, and black pepper."),
        new("2", "Chicken Tikka Masala", "A dish of roasted chicken chunks in a spicy sauce."),
        new("3", "Beef Wellington", "A beef fillet coated with pâté and duxelles, which is then wrapped in puff pastry."),
        new("4", "Sushi", "A dish of vinegared rice topped with raw fish or other ingredients."),
        new ("5", "Pad Thai", "A stir-fried rice noodle dish commonly served as a street food and at casual local eateries in Thailand."),
        new ("6", "Tacos", "A traditional Mexican dish consisting of a corn or wheat tortilla folded or rolled around a filling.")
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