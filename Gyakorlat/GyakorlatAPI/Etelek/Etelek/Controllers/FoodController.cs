using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Etelek.Model;
using Microsoft.AspNetCore.Mvc;

namespace Etelek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly FoodDataContext foodDataContext;

        public FoodController(FoodDataContext foodDataContext)
        {
            this.foodDataContext = foodDataContext;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Food>> Get()
        {
            return foodDataContext.Food.ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Food> Get(int id)
        {
            Food f = foodDataContext.Food.FirstOrDefault(food => food.ID == id);

            if (f == null)
                return NotFound();

            return f;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Food value, CancellationToken cancellationToken)
        {
            if (foodDataContext.Food.Count() > 100)
                return BadRequest();

            Food existingName = foodDataContext.Food.FirstOrDefault(food => food.Name == value.Name);

            if (existingName != null)
                return Conflict();

            foodDataContext.Food.Add(value);
            await foodDataContext.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Food value, CancellationToken cancellationToken)
        {
            Food f = foodDataContext.Food.FirstOrDefault(food => food.ID == id);

            if (id != value.ID)
                return BadRequest();

            if (f == null)
                return NotFound();

            f.Name = value.Name;
            f.Difficulty = value.Difficulty;
            f.FoodType = value.FoodType;

            await foodDataContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            Food f = foodDataContext.Food.FirstOrDefault(food => food.ID == id);

            if (f == null)
                return NotFound();

            foodDataContext.Food.Remove(f);
            await foodDataContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
