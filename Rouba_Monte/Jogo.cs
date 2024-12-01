using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rouba_Monte
{
    class Jogo
    {
        public Stack<Carta> baralho {  get; set; }
        public Queue<Jogador> jogadores;

        public Jogo(int numDeCartas, int numDeJogador)
        {
            baralho = new Stack<Carta>(numDeCartas);
            jogadores= new Queue<Jogador>(numDeJogador);
            IniciarBaralho();

        }
        private void IniciarBaralho()
        {
            List<Carta> list = new List<Carta>();
            string[] naipes = { "♠", "♣", "♥", "♦" };
            foreach (string naipe in naipes)
            {
                for (int valor = 1; valor <= 13; valor++)
                {
                    list.Add(new Carta(valor, naipe));
                }
            }

            Shuffle(list);

            foreach (var carta in list)
            {
                baralho.Push(carta);
            }
            Console.WriteLine("Baralho Criado Com Sucesso");
        }

        private void Shuffle(List<Carta> list) // Fisher-Yates Shuffle
        {
            Random random = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int randomIndex = random.Next(i + 1);
                Carta temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }
    }
}
