using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OdeToFood.Data.Models;

namespace OdeToFood.Data.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }
        public void Add(Restaurant restaurant)
        {
            db.Restaurants.Add(restaurant);
            db.SaveChanges();
        }

        public void Update(Restaurant restaurant)
        {
            //var r = Get(restaurant.Id);
            //r.Name = restaurant.Name;
            //r.Cuisine = restaurant.Cuisine;
            //db.SaveChanges();
            var entry = db.Entry(restaurant);
            entry.State = EntityState.Modified;
            db.SaveChanges();
        }

        public Restaurant Get(int id)
        {
            return db.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            //return db.Restaurants;
            //return db.Restaurants.Orderby ...
            return from r in db.Restaurants
                   orderby r.Name
                   select r;
        }

        public void Delete(int id)
        {
            var restaurant = db.Restaurants.Find(id); 
            if(restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
                db.SaveChanges();
            }
        }
    }
}
