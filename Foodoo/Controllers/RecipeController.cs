using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodoo.Data;
using Foodoo.Logic;
using Foodoo.Models;
using Foodoo.Models.ApiModels;
using Foodoo.Models.DataModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Foodoo.Controllers
{
    [EnableCors("AllowCORS")]
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly FoodooContext context;
        private IWebHostEnvironment environment;
        public RecipeController(FoodooContext _context, IWebHostEnvironment _environment)
        {
            context = _context;
            environment = _environment;
        }
        
        [HttpGet]
        public List<ApiRecipeUpload> GetPageRecipes()
        {
            return new RecipeLogic(context).GetRecipes();
        }

        [HttpPost("/recipe")]
        public void AddRecipe(IFormCollection collection)
        {
            var image = Request.Form.Files[0];
            
            ApiRecipeUpload model = new()
            {
                Name = collection["name"],
                Carbs = collection["carbs"],
                Description = collection["description"],
                Ingredients = collection["ingredients"],
                Preparation = collection["preparation"]
            };
            new RecipeLogic(context).AddNewRecipe(model, image, environment.WebRootPath);
        }

        [HttpGet("/recipe/{id}")]
        public RecipeModel GetRecipeById(string id)
        {
            return new RecipeLogic(context).GetRecipeById(id);
        }
    }
    
   
}