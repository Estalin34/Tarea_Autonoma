using Tarea_Autonoma.Models;
using Newtonsoft.Json;

namespace Tarea_Autonoma.Services
{
    public class PokemonService
    {
        private readonly HttpClient _httpClient;
        private readonly string apiURL = "https://pokeapi.co/api/v2/pokemon";
        private const int PageSize = 20; // Cantidad de Pokémon por página

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PokemonPage> GetPokemonsAsync(int page)
        {
            int offset = (page - 1) * PageSize;
            var response = await _httpClient.GetStringAsync($"{apiURL}?limit={PageSize}&offset={offset}");
            var result = JsonConvert.DeserializeObject<PokemonListResponse>(response);

            var pokemons = new List<Pokemon>();

            foreach (var item in result.Results)
            {
                var pokemonData = await GetPokemonAsync(item.Name);
                pokemons.Add(pokemonData);
            }

            return new PokemonPage
            {
                Pokemons = pokemons,
                CurrentPage = page,
                NextPage = result.Next != null ? page + 1 : null,
                PreviousPage = result.Previous != null && page > 1 ? page - 1 : null
            };
        }

        public async Task<Pokemon> GetPokemonAsync(string name)
        {
            var response = await _httpClient.GetStringAsync($"{apiURL}/{name}");
            var pokemon = JsonConvert.DeserializeObject<Pokemon>(response);
            return pokemon;
        }
    }

    public class PokemonListResponse
    {
        public List<PokemonBasic> Results { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
    }

    public class PokemonBasic
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class PokemonPage
    {
        public List<Pokemon> Pokemons { get; set; }
        public int CurrentPage { get; set; }
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }
    }
}
