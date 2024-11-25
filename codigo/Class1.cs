using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo
{
    internal class Mesa
    {
        public Queue<Jogador> Jogadores;
        public List<Carta> Carta;
        public Baralho Monte; 
        public int numDeJogador;
        public Mesa(int numDeJogador, int numDeBaralhos)
        {
            Jogadores = new Queue<Jogador>(numDeJogador);
            Carta = new List<Carta>();
            Monte = new Baralho(numDeBaralhos);
            this.numDeJogador = numDeJogador;
        }
        public void ColocarPlayer(int numdeJogador)
        {
            for (int i = 0; i < numdeJogador; i++)
            {
                Console.WriteLine($"Insira o nome do jogador {i + 1}");
                Jogador J = new Jogador(Console.ReadLine());
                Jogadores.Enqueue(J);
            }
            foreach (var jogador in Jogadores) { Console.WriteLine(jogador.Nome); }
        }
        public Carta ComprarCartaDaVez() 
        {
            Carta cartaDaVez = Monte.Pop();
            return cartaDaVez; 
        }
        public void Ação1(Jogador jogadorAtual)
        {
            Carta cartadavez = Monte.Pop();
            foreach (var jogador in Jogadores)
            {
                if(cartadavez == jogador.monte.ExibirTopo())
                {
                    JogadorAtual.PegarOMonte(jogador);
                }
            }
        }
    }
}
