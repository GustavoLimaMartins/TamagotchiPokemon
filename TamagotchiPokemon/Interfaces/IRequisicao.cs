using System.Text.Json;

namespace TamagotchiPokemon.Interfaces
{
    public interface IRequisicao
    {
        public async Task<string> RequisicaoHabilidade(string index)
        {
            using (HttpClient client = new())
            {
                var url = $"https://pokeapi.co/api/v2/ability/{index}/";
                string resposta = await client.GetStringAsync(url);
                return resposta;
            }
        }
        public async Task<string> RequisicaoFisionomia(string index)
        {
            using (HttpClient client = new())
            {
                var url = $"https://pokeapi.co/api/v2/pokemon/{index}/";
                string resposta = await client.GetStringAsync(url);
                return resposta;
            }
        }
        public JsonDocument RequisicaoToJson(string respostaReq)
        {
            JsonElement requisicao = JsonSerializer.Deserialize<JsonElement>(respostaReq);
            var json = JsonDocument.Parse(requisicao.ToString());
            return json;
        }
    }
}
