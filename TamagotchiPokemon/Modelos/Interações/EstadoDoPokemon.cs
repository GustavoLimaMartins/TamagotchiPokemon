namespace TamagotchiPokemon.Modelos.Interações
{
    public class EstadoDoPokemon
    {
        public EstadoDoPokemon(int index, string nomePok)
        {
            Index = index;
            IndexPokemons.Add(index);
            NomeDoPokemon.Add(nomePok);
            Humor.Add(5);
            Alimentacao.Add(5);
        }
        public int Index { get; set; }
        public List<string> NomeDoPokemon { get; set; } = new List<string>();
        public List<int> IndexPokemons { get; set; } = new List<int>();
        public List<int> Humor { get; set; } = new List<int>();
        public List<int> Alimentacao { get; set; } = new List<int>();

        public void BrincarMascote()
        {
            Humor[Index] = Humor[Index] + 1;
            Alimentacao[Index] = Alimentacao[Index] - 1;
        }
        public void AlimentarMascote()
        {
            Alimentacao[Index] = Alimentacao[Index] + 1;
            Humor[Index] = Humor[Index] - 1;
        }
        public void EstadoMascote()
        {
            if (Humor[Index] < 0)
            {
                Humor[Index] = 0;
            }
            if (Alimentacao[Index] < 0)
            {
                Alimentacao[Index] = 0;
            }

            Console.WriteLine($"Estado do Pokemon (De 1 a 10): ");
            Console.WriteLine($"Alimentação: {Alimentacao[Index].ToString()}");
            Console.WriteLine($"Humor: {Humor[Index].ToString()}");

            if (Alimentacao[Index] <= 4)
            {
                Console.WriteLine($"{NomeDoPokemon[Index]} está com fome!");
            }
            else
            {
                Console.WriteLine($"{NomeDoPokemon[Index]} está satisfeito!");
            }
            if (Humor[Index] >= 5)
            {
                Console.WriteLine($"{NomeDoPokemon[Index]} está feliz!");
            }
            else 
            {
                Console.WriteLine($"{NomeDoPokemon[Index]} está triste!");
            }
        }
    }
}
