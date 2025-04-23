using System.Text.Json;

namespace TamagotchiPokemon.API
{
    public class Requisicao
    {
        public Requisicao()
        {
            Url = "https://pokeapi.co/api/v2/pokemon";
        }
        private string Url { get; set; }
        private JsonElement Resultados { get; set; }
        public List<string> IndexEncontrado { get; set; } = new List<string>();
        public List<string> NomePokemon { get; set; } = new List<string>();

        public async Task<string> RealizarRequisicaoPokemon()
        {
            using (HttpClient client = new())
            {
                string resposta = await client.GetStringAsync(Url);
                JsonElement requisicao = JsonSerializer.Deserialize<JsonElement>(resposta);
                var json = JsonDocument.Parse(requisicao.ToString());
                json.RootElement.TryGetProperty("results", out JsonElement result);
                json.RootElement.TryGetProperty("next", out JsonElement endpoint);
                json.RootElement.TryGetProperty("count", out JsonElement count);
                Resultados = result;
                Url = endpoint.ToString();
                return resposta;
            }
        }

        public void ProcurarPokemon(string pokemon)
        {
            for (var i = 0; i < Resultados.GetArrayLength(); i++)
            {
                Resultados[i].TryGetProperty("name", out JsonElement nome);
                Resultados[i].TryGetProperty("url", out JsonElement url);

                if (nome.ToString().ToUpper() == pokemon.ToUpper())
                {
                    var nomeFormatt = nome.ToString().ToUpper();
                    string indexFormatt;

                    NomePokemon.Add(nomeFormatt);
                    if (url.ToString().Length == 36)
                    {
                        indexFormatt = $"{url.ToString()[url.ToString().Length - 2]}";
                        IndexEncontrado.Add(indexFormatt);
                        //Console.WriteLine($"Pokemon {nomeFormatt}: ({indexFormatt})");
                        break;
                    }
                    else
                    {
                        indexFormatt = $"{url.ToString()[url.ToString().Length - 3]}{url.ToString()[url.ToString().Length - 2]}";
                        IndexEncontrado.Add(indexFormatt);
                        //Console.WriteLine($"Pokemon {nomeFormatt}: ({indexFormatt})");
                        break;
                    }
                }

                if (i == Resultados.GetArrayLength() - 1)
                {
                    i = 0;
                    RealizarRequisicaoPokemon().Wait();
                }
            }
        }
    }
}
