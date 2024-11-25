using System;

namespace Jogo
{
    internal class Jogador
    {
        public string Nome { get; set; }
        private Fila ranking;
        public int Posicao { get; set; }
        public Baralho monte;

        public Jogador(string nome)
        {
            Nome = nome;
            ranking = new Fila(5);
            Posicao = 0;
            monte = new Baralho();
        }

        public void AdicionarRanking()
        {
            if (ranking.IsQueueIsFull())
            {
                ranking.Remover();
            }
            ranking.Inserir(Posicao);
        }

        public void PegarOMonte(Baralho destino, Baralho origem)
        {
            Pilha tmp = new Pilha();
            while (origem.Topo != null)
            {
                Carta carta = origem.Pop();
                tmp.Push(carta);
            }
            while (tmp.Topo != null)
            {
                Carta carta = tmp.Pop();
                destino.Push(carta);
            }
        }
    }
}
