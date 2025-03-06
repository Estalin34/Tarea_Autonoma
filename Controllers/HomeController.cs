using Microsoft.AspNetCore.Mvc;
using Tarea_Autonoma.Models;
using Tarea_Autonoma.Services;
using System.Threading.Tasks;

namespace Tarea_Autonoma.Controllers
{
    public class HomeController : Controller
    {
        private readonly PokemonService _pokemonService;

        public HomeController(PokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        public async Task<IActionResult> Index(int page = 1) 
        {
            var pokemonPage = await _pokemonService.GetPokemonsAsync(page);
            return View(pokemonPage);
        }

        public async Task<IActionResult> Details(string name)
        {
            var pokemon = await _pokemonService.GetPokemonAsync(name);
            return View(pokemon);
        }
    }
}
