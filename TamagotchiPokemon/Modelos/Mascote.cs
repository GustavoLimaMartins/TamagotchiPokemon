using TamagotchiPokemon.API;
using TamagotchiPokemon.Interfaces;
using TamagotchiPokemon.Modelos.Caracteristicas;

namespace TamagotchiPokemon.Modelos
{
    public class Mascote
    {
        public Mascote(string index)
        {
            Index = index;
        }
        public List<Requisicao> Req { get; set; } = new List<Requisicao>();
        public List<Habilidade> Habilidades { get; set; } = new List<Habilidade>();
        public List<Fisionomia> Fisionomias { get; set; } = new List<Fisionomia>();
        public string Index { get; set; }

        internal async Task<string> SalvarDadosPokemon(IRequisicao subReq, Requisicao req)
        {
            Habilidade habilidade = new Habilidade(Index);
            Fisionomia fisionomia = new Fisionomia(Index);
            await habilidade.AdicionarHabilidade(subReq);
            await fisionomia.AdicionarPeso(subReq);
            await fisionomia.AdicionarAltura(subReq);
            Req.Add(req);
            Habilidades.Add(habilidade);
            Fisionomias.Add(fisionomia);
            return subReq.ToString();
        }
    }
}
