using System;

namespace Jogo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao Jogo!");
            Console.WriteLine("Insira o número de jogadores:");
            int numJogadores = int.Parse(Console.ReadLine());
            Console.WriteLine("Insira o número de baralhos:");
            int numBaralhos = int.Parse(Console.ReadLine());
            Mesa mesa = new Mesa(numJogadores, numBaralhos);
            while (true)
            {
                Jogador jogadorAtual = mesa.Jogadores.Dequeue();
                Console.WriteLine($"\nVez de {jogadorAtual.Nome}");
                mesa.RealizarJogada(jogadorAtual);
                mesa.Jogadores.Enqueue(jogadorAtual);
                Console.WriteLine("\nPressione 'Enter' para a próxima rodada ou 'Q' para sair.");
                string entrada = Console.ReadLine();
                if (entrada?.ToUpper() == "Q")
                {
                    Console.WriteLine("Fim do jogo!");
                    break;
                }
            }
        }
    }
}
