using System.Text;
using System.Text.Json;
using TamagotchiPokemon.API;
using TamagotchiPokemon.Interfaces;
using TamagotchiPokemon.Modelos;
using TamagotchiPokemon.Modelos.Interações;

namespace TamagotchiPokemon.Views
{
    public class Menu : IRequisicao
    {
        public EstadoDoPokemon Estado = new EstadoDoPokemon(0, "Pokemon");
        public string NomeUsuario { get; set; }
        public List<string> NomeDoPokemon { get; set; } = new List<string>();
        public void BoasVindas()
        {
            Console.WriteLine("Olá, boas vindas ao Pokemon-Tamagotchi Game!");
            Console.Write("Digite seu nome: ");
            NomeUsuario = Console.ReadLine()!;
            try
            {
                if (string.IsNullOrEmpty(NomeUsuario))
                {
                    throw new Exception("Nome inválido, digite novamente.");
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                BoasVindas();
            }
        }
        public int MenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("###### Menu ######");
            string art = File.ReadAllText("C:\\Users\\Gustavo Lima\\Desktop\\Alura\\C#\\TamagotchiPokemon\\TamagotchiPokemon\\pikachu.txt", Encoding.UTF8);
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(art);
            Console.WriteLine("Digite (1) para 'Capturar Pokemon'");
            Console.WriteLine("Digite (2) para 'Ver Pokemon capturados'");
            Console.WriteLine("Digite (3) para 'Conhecer mais sobre um Pokemon'");
            Console.WriteLine("Digite (4) para 'Interagir com Pokemon'");
            Console.WriteLine("Digite (0) para 'Sair'");
            Console.Write($"\nOlá {NomeUsuario.ToUpper()}, escolha uma opção: ");
            int responseUser = -1;

            try
            {
                responseUser = int.Parse(Console.ReadLine()!);
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Console.WriteLine("Digite apenas números de 0 a 4! Aperte qualquer tecla para tentar novamente.");
                Console.ReadKey();
                responseUser = MenuPrincipal();
            }
            
            if (responseUser < 0 || responseUser > 4)
            {
                Console.Clear();
                Console.WriteLine("Opção inválida, aperte qualquer tecla para tentar novamente.");
                Console.ReadKey();
                responseUser = MenuPrincipal();
            }
            return responseUser;
        }
        public int SubMenuPokemon() 
        {
            for (int i = 0; i < NomeDoPokemon.Count; i++)
            {
                Console.WriteLine($"Digite ({i}) para {NomeDoPokemon[i]}");
            }
            Console.Write("\nEscolha um Pokemon para interagir: ");
            int responseUser = -1;

            try
            {
                responseUser = int.Parse(Console.ReadLine()!);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Digite apenas números! Aperte qualquer tecla para tentar novamente.");
                Console.ReadKey();
                responseUser = SubMenuPokemon();
            }
            if (responseUser < 0 || responseUser > NomeDoPokemon.Count - 1)
            {
                Console.WriteLine($"Apenas números inteiros de 0 a {(NomeDoPokemon.Count - 1).ToString().Replace("-1", "0")} estão disponíveis! Aperte qualquer tecla para tentar novamente.");
                Console.ReadKey();
                responseUser = SubMenuPokemon();
            }
            Console.Clear();
            return responseUser;
        }
        public int InteracaoPokemon(string nomePok)
        {
            Console.WriteLine($"{NomeUsuario}, você deseja:");
            Console.WriteLine($"Digite (1): Brincar com {nomePok}");
            Console.WriteLine($"Digite (2): Alimentar o {nomePok}");
            Console.WriteLine($"Digite (3): Saber como está o {nomePok}");
            Console.WriteLine("Digite (0): Voltar");
            Console.Write("Escolha uma das opções: ");
            return int.Parse(Console.ReadLine()!);
        }
        public void OpcoesMenu()
        {
            var opcao = MenuPrincipal();
            while (opcao > -1)
            {
                if (opcao == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Até a próxima :) ...");
                    break;
                }
                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Capturando Pokemon...");
                        CapturandoPokemon().Wait();
                        opcao = MenuPrincipal();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine($"Lista de Pokemon(s) Capturado(s) - Quantidade ({NomeDoPokemon.Count}):");
                        VerPokemonCapturados();
                        Console.WriteLine("\nAperte qualquer tecla para retornar...");
                        Console.ReadKey();
                        opcao = MenuPrincipal();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Conhecendo mais sobre o Pokemon...");
                        PesquisarPokemon().Wait();
                        Console.WriteLine("\nAperte qualquer tecla para retornar...");
                        Console.ReadKey();
                        opcao = MenuPrincipal();
                        break;

                    case 4:
                        Console.Clear();
                        if (NomeDoPokemon.Count == 0)
                        {
                            Console.WriteLine("Nenhum Pokemon capturado, volte ao menu principal e capture um Pokemon!");
                            Console.WriteLine("\nAperte qualquer tecla para retornar ao menu principal...");
                            Console.ReadKey();
                            opcao = MenuPrincipal();
                            break;
                        }
                        Console.WriteLine($"Lista de Pokemon(s) para interagir - Quantidade ({NomeDoPokemon.Count}):");
                        var index = SubMenuPokemon();
                        var pokemonSelect = NomeDoPokemon[index];
                        Console.Clear();
                        var interacaoSelect = InteracaoPokemon(pokemonSelect);
                        Console.Clear();
                        
                        switch (interacaoSelect)
                        {
                            case 1:
                                Estado.BrincarMascote();
                                Console.WriteLine($"{pokemonSelect} recebeu +1 ponto de humor!");
                                break;
                            case 2:
                                Estado.AlimentarMascote();
                                Console.WriteLine($"{pokemonSelect} recebeu +1 ponto de alimentação!");
                                break;
                            case 3:
                                Estado.EstadoMascote();
                                break;
                            default:
                                break;
                        }
                        Console.WriteLine("\nAperte qualquer tecla para retornar ao menu principal...");
                        Console.ReadKey();
                        opcao = MenuPrincipal();
                        break;
                    default:
                        break;
                }
            }
        }
        public void VerPokemonCapturados()
        {
            foreach (var item in NomeDoPokemon)
            {
                Console.WriteLine(item);
            }
        }
        public async Task<string> PesquisarPokemon()
        {
            Requisicao req = new Requisicao();
            IRequisicao requisicao = new Menu();
            
            Console.Write("Digite o nome do Pokemon que deseja conhecer: ");
            req.RealizarRequisicaoPokemon().Wait();
            req.ProcurarPokemon(Console.ReadLine()!);
            string fisionomia = await requisicao.RequisicaoFisionomia(req.IndexEncontrado[0].ToString());
            string habilidade = await requisicao.RequisicaoHabilidade(req.IndexEncontrado[0].ToString());
            var jsonFisionomia = requisicao.RequisicaoToJson(fisionomia);
            var jsonHabilidade = requisicao.RequisicaoToJson(habilidade);
            jsonFisionomia.RootElement.TryGetProperty("weight", out JsonElement peso);
            jsonFisionomia.RootElement.TryGetProperty("height", out JsonElement altura);
            jsonHabilidade.RootElement.TryGetProperty("name", out JsonElement efeitoNome);
            jsonHabilidade.RootElement.TryGetProperty("effect_entries", out JsonElement efeito);
            efeito[1].TryGetProperty("effect", out JsonElement eff);

            Console.WriteLine("\nDados do Pokemon procurado:\n");
            Console.WriteLine($"Pokemon Nº{req.IndexEncontrado[0]}: {req.NomePokemon[0].ToString().ToUpper()}");
            Console.WriteLine($"Altura: {altura} cm");
            Console.WriteLine($"Peso: {peso} kg");
            Console.WriteLine($"Habilidade: {efeitoNome}\n");
            Console.WriteLine($"Efeito: {eff.ToString().Replace("\n", " ")}");
            req.NomePokemon.Clear();
            req.IndexEncontrado.Clear();
            return req.ToString();
        }
        public async Task<string> CapturandoPokemon()
        {
            Requisicao req = new Requisicao();
            IRequisicao subReq = new Menu();
            
            await req.RealizarRequisicaoPokemon();
            Console.Write("Digite o nome do Pokemon que deseja capturar: ");
            req.ProcurarPokemon(Console.ReadLine()!);

            foreach (var item in req.IndexEncontrado)
            {
                Console.Clear();
                string art = File.ReadAllText("C:\\Users\\Gustavo Lima\\Desktop\\Alura\\C#\\TamagotchiPokemon\\TamagotchiPokemon\\bolaPok.txt", Encoding.UTF8);
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine(art);
                Console.WriteLine("Dados do Pokemon capturado:\n");
                Mascote mascote = new Mascote(item);
                await mascote.SalvarDadosPokemon(subReq, req);

                for (int i = 0; i < mascote.Req.Count; i++)
                {
                    NomeDoPokemon.Add(mascote.Req[i].NomePokemon[i].ToString().ToUpper());
                    Console.WriteLine($"Pokemon Nº{mascote.Req[i].IndexEncontrado[i]}: {mascote.Req[i].NomePokemon[i].ToString().ToUpper()}");
                    Console.WriteLine($"Altura: {mascote.Fisionomias[i].Altura[i].ToString()} cm");
                    Console.WriteLine($"Peso: {mascote.Fisionomias[i].Peso[i].ToString()} kg");
                    Console.WriteLine($"Habilidade: {mascote.Habilidades[i].Nome[i].ToString()}\n");
                    Console.WriteLine($"Efeito: {mascote.Habilidades[i].Efeito[i].ToString()}");

                    Estado = new EstadoDoPokemon(i, mascote.Req[i].NomePokemon[i].ToString().ToUpper());
                }
            }
            Console.WriteLine("\nAperte qualquer tecla para retornar...");
            Console.ReadKey();
            return req.ToString();
        }
    }
}
