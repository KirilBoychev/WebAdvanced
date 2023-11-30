using HouseRentingSystem.Contracts.Agent;
using HouseRentingSystem.Infrastructure;
using HouseRentingSystem.Models.Agents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class AgentController : Controller
    {
        private readonly IAgentService _agents;
        public AgentController(IAgentService agents)
        {
            this._agents = agents;
        }
        [HttpGet]
        public async Task<IActionResult> Become()
        {
            if (await _agents.ExistById(User.Id()))
            {
                return BadRequest();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            var userId = User.Id();

            if (await _agents.ExistById(userId))
            {
                return BadRequest();
            }

            if (await _agents.UserWithPhoneNumberExists(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "Phone number already exists. Enter another one.");
            }

            if (await _agents.UserHasRent(userId))
            {
                ModelState.AddModelError("Error", "You should have no rents to become an agent!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _agents.Create(userId, model.PhoneNumber);

            return RedirectToAction(nameof(HousesController.All), "Houses");
        }
    }
}
