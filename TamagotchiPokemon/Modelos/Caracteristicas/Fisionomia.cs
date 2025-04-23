
using System.Text.Json;
using TamagotchiPokemon.Interfaces;

namespace TamagotchiPokemon.Modelos.Caracteristicas
{
    public class Fisionomia : IRequisicao
    {
        public Fisionomia(string i)
        {
            Index = i;
        }
        public List<int> Altura { get; set; } = new List<int>();
        public List<int> Peso { get; set; } = new List<int>();
        public string Index { get; set; }
        
        public async Task<string> AdicionarPeso(IRequisicao req) 
        {
            using (HttpClient client = new HttpClient())
            {
                string resposta = await req.RequisicaoFisionomia(Index);
                var json = req.RequisicaoToJson(resposta);
                json.RootElement.TryGetProperty("weight", out JsonElement peso);
                Peso.Add(peso.GetInt32());
                return resposta;
            }
        }
        public async Task<string> AdicionarAltura(IRequisicao req)
        {
            using (HttpClient client = new HttpClient())
            {
                string resposta = await req.RequisicaoFisionomia(Index);
                var json = req.RequisicaoToJson(resposta);
                json.RootElement.TryGetProperty("height", out JsonElement altura);
                Altura.Add(altura.GetInt32());
                return resposta;
            }
        }
    }
}