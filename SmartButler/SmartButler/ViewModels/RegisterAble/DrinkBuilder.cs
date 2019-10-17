using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SmartButler.Models;
using SmartButler.Services;
using Xamarin.Forms;

namespace SmartButler.ViewModels.RegisterAble
{
    public interface IDrinkBuilder
    {
        IDrinkBuilder Default();
        IDrinkBuilder Default(Drink drink);
        IDrinkBuilder Default(string name, string partialResource, Type resolvingType);

        IDrinkBuilder SetName(string name);
        IDrinkBuilder SetImageSource(string partialResource, Type resolvingType);

        IDrinkBuilder SetIngredients(List<Ingredient> ingredients);
        IDrinkBuilder AddIngredients(params Ingredient[] ingredients);
        IDrinkBuilder AddIngredient(Ingredient ingredient);

        Drink Build();
    }

    public class DrinkBuilder : IDrinkBuilder
    {
        private Drink _drink = new Drink();


        public IDrinkBuilder Default()
        {
            _drink = new Drink();
            return this;
        }

        public IDrinkBuilder Default(Drink drink)
        {
            if (drink == null) ExceptionFactory.Get<ArgumentNullException>("'drink' is null!");

            _drink = drink;

            return this;
        }

        public IDrinkBuilder Default(string name, string partialResource, Type resolvingType)
        {
            if (string.IsNullOrWhiteSpace(name)) throw ExceptionFactory.Get<ArgumentException>("'name' is null or has whitespaces!");
            if (resolvingType == null) throw ExceptionFactory.Get<ArgumentNullException>("'resolvingType' is null");
            if (string.IsNullOrWhiteSpace(partialResource))
                throw ExceptionFactory.Get<ArgumentException>("'partialResource' is null or has whitespaces");

            _drink.Name = name;

            var sourceAssembly = resolvingType.GetTypeInfo().Assembly;
            var resource = string.Join(".", "SmartButler.Resources", partialResource);
            _drink.ActualImage = ImageSource.FromResource(resource, sourceAssembly);

            return this;
        }

        public IDrinkBuilder SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw ExceptionFactory.Get<ArgumentException>("'name' is null or has whitespaces!");

            _drink.Name = name;
            return this;
        }

        public IDrinkBuilder SetImageSource(string partialResource, Type resolvingType)
        {
            if (resolvingType == null) throw ExceptionFactory.Get<ArgumentNullException>("'resolvingType' is null");
            if (string.IsNullOrWhiteSpace(partialResource))
                throw ExceptionFactory.Get<ArgumentException>("'partialResource' is null or has whitespaces");

            var sourceAssembly = resolvingType.GetTypeInfo().Assembly;
            var resource = string.Join(".", "SmartButler.Resources", partialResource);

            var imageSource = ImageSource.FromResource(resource, sourceAssembly);

            _drink.ActualImage = imageSource;

            return this;
        }

        public IDrinkBuilder SetIngredients(List<Ingredient> ingredients)
        {
            if (ingredients == null) throw ExceptionFactory.Get<ArgumentNullException>("'ingredients' is null!");
            if (ingredients.Count <= 0) throw ExceptionFactory.Get<ArgumentException>("'ingredients' is empty!");
            if (ingredients.Any(ingredient => ingredient == null))
                throw ExceptionFactory.Get<ArgumentNullException>("Any ingredient of 'ingredients' ins null!");

            _drink.Ingredients = ingredients;
            return this;
        }

        public IDrinkBuilder AddIngredients(params Ingredient[] ingredients)
        {
            if (ingredients == null) throw ExceptionFactory.Get<ArgumentNullException>("'ingredients' is null!");
            if (ingredients.Length <= 0) throw ExceptionFactory.Get<ArgumentException>("'ingredients' is empty!");
            if (ingredients.Any(ingredient => ingredient == null))
                throw ExceptionFactory.Get<ArgumentNullException>("Any ingredient of 'ingredients' ins null!");

            foreach (var ingredient in ingredients)
                _drink.Ingredients.Add(ingredient);

            return this;
        }

        public IDrinkBuilder AddIngredient(Ingredient ingredient)
        {
            if (ingredient == null) throw ExceptionFactory.Get<ArgumentNullException>("'ingredient' is null");

            _drink.Ingredients.Add(ingredient);
            return this;
        }

        public Drink Build()
        {
            if (_drink == null) throw ExceptionFactory.Get<NullReferenceException>("'_drink' is null!");

            return _drink;
        }
    }
}
