using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodoo.Data;
using Foodoo.Models;
using Microsoft.AspNetCore.Cors;
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

        public RecipeController(FoodooContext _context)
        {
            context = _context;
        }

        [HttpPost("/recipe/add")]
        public RecipeModel AddRecipe(RecipeModel model)
        {
            model.Id = Guid.NewGuid();
            new RecipeData(context).AddRecipe(model);
            return model;
        }
    }
}