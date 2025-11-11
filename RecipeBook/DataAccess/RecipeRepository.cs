using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using RecipeBook.Models;

namespace RecipeBook.DataAccess
{
    public class RecipeRepository
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["RecipeBookDB"].ConnectionString;

        // Get all recipes
        public List<Recipe> GetAllRecipes()
        {
            var recipes = new List<Recipe>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Recipes ORDER BY CreatedAt DESC", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    recipes.Add(new Recipe
                    {
                        RecipeID = (int)reader["RecipeID"],
                        Title = reader["Title"].ToString(),
                        Ingredients = reader["Ingredients"].ToString(),
                        Instructions = reader["Instructions"].ToString(),
                        CreatedAt = (DateTime)reader["CreatedAt"]
                    });
                }
            }

            return recipes;
        }

        // Add a new recipe
        public void AddRecipe(Recipe recipe)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Recipes (Title, Ingredients, Instructions) VALUES (@Title, @Ingredients, @Instructions)", conn);
                cmd.Parameters.AddWithValue("@Title", recipe.Title);
                cmd.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                cmd.Parameters.AddWithValue("@Instructions", recipe.Instructions);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Get a single recipe by ID
        public Recipe GetRecipeById(int recipeID)
        {
            Recipe recipe = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Recipes WHERE RecipeID = @RecipeID", conn);
                cmd.Parameters.AddWithValue("@RecipeID", recipeID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    recipe = new Recipe
                    {
                        RecipeID = (int)reader["RecipeID"],
                        Title = reader["Title"].ToString(),
                        Ingredients = reader["Ingredients"].ToString(),
                        Instructions = reader["Instructions"].ToString(),
                        CreatedAt = (DateTime)reader["CreatedAt"]
                    };
                }
            }

            return recipe;
        }

        // Update an existing recipe
        public void UpdateRecipe(Recipe recipe)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Recipes SET Title = @Title, Ingredients = @Ingredients, Instructions = @Instructions WHERE RecipeID = @RecipeID", conn);
                cmd.Parameters.AddWithValue("@RecipeID", recipe.RecipeID);
                cmd.Parameters.AddWithValue("@Title", recipe.Title);
                cmd.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                cmd.Parameters.AddWithValue("@Instructions", recipe.Instructions);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Delete a recipe by ID
        public void DeleteRecipe(int recipeID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Recipes WHERE RecipeID = @RecipeID", conn);
                cmd.Parameters.AddWithValue("@RecipeID", recipeID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
