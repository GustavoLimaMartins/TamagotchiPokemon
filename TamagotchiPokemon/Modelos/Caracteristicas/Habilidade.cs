using System.Text.Json;
using TamagotchiPokemon.Interfaces;

namespace TamagotchiPokemon.Modelos.Caracteristicas
{
    public class Habilidade
    {
        public Habilidade(string i) 
        {
            Index = i;
        }
        public List<string> Nome { get; set; } = new List<string>();
        public List<string> Efeito { get; set; } = new List<string>();
        private string Index { get; set; }

        public async Task<string> AdicionarHabilidade(IRequisicao req)
        {
            using (HttpClient client = new())
            {
                string resposta = await req.RequisicaoHabilidade(Index);
                var json = req.RequisicaoToJson(resposta);
                json.RootElement.TryGetProperty("name", out JsonElement result);
                json.RootElement.TryGetProperty("effect_entries", out JsonElement efeito);
                efeito[1].TryGetProperty("effect", out JsonElement eff);
                Nome.Add(result.ToString().ToUpper());
                Efeito.Add(eff.ToString().Replace("\n", " "));
                return resposta;
            }
        }
    }
}
