using OdeToFood.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id = 1, Name = "Scott's Pizza", Cuisine = CuisineType.Italian},
                new Restaurant {Id = 2, Name = "Tersiguels", Cuisine = CuisineType.French},
                new Restaurant {Id = 3, Name = "Mango Grove", Cuisine = CuisineType.Italian}
            };
        }

        public Restaurant Get(int id)
        {
            return restaurants.FirstOrDefault(r => r.Id == id);//default will be null
            //because restaurant is a reference type
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return restaurants.OrderBy(r => r.Name);//Ascending order by default. 
        }

        public void Add(Restaurant restaurant)
        {
            restaurants.Add(restaurant);
            //generate id for this restaurant this is only for inmemory data. 
            restaurant.Id = restaurants.Max(r => r.Id) + 1;
        }

        public void Update(Restaurant restaurant)
        {
            foreach(var item in restaurants)
            {
                if (item.Id == restaurant.Id)
                {
                    item.Name = restaurant.Name;
                    item.Cuisine = restaurant.Cuisine;
                    break;//once found stop iterations
                }
            }
        }

        public void Delete(int id)
        {
            var restaurant = Get(id);
            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
        }
    }
}
