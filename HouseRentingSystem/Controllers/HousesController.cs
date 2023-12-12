using HouseRentingSystem.Contracts.Agent;
using HouseRentingSystem.Contracts.House;
using HouseRentingSystem.Infrastructure;
using HouseRentingSystem.Models.Houses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class HousesController : Controller
    {
        private readonly IHouseService _houses;
        private readonly IAgentService _agents;
        public HousesController(IHouseService houses, IAgentService agents)
        {
            this._houses = houses;
            this._agents = agents;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View(new AllHousesQueryModel());
        }

        [Authorize]
        public async Task<IActionResult> Mine()
        {
            return View(new AllHousesQueryModel());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(new HouseDetailsViewModel());
        }

        [Authorize]
        public async Task<IActionResult> Add()
        {
            if (await _agents.ExistById(User.Id()) == false)
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }
            return View(new HouseFormModel
            {
                Categories = await _houses.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            if (await _agents.ExistById(User.Id()) == false)
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            if (await _houses.CategoryExist(model.CategoryId) == false)
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await _houses.AllCategories();

                return View(model);
            }

            var agentId = await _agents.GetAgentId(User.Id());

            var newHouseId = _houses.Create(model.Title, model.Address, model.Description, model.ImageUrl,
                model.PricePerMonth, model.CategoryId, agentId);

            return RedirectToAction(nameof(Details),  new { id = newHouseId });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            return View(new HouseFormModel());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, HouseFormModel house)
        {
            return RedirectToAction(nameof(Details), new { id = "1" });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return View(new HouseDetailsViewModel());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(HouseDetailsViewModel house)
        {
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Rent(int id)
        {
            return RedirectToAction(nameof(Mine));
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Leave(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
    }
}
