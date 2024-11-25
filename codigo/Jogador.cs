using System;

namespace Jogo
{
    internal class Jogador
    {
        public string Nome { get; set; }
        private Fila ranking;
        public int qtdCartasdoMonte { get; set; }
        public int posicao { get; set;}
        public Baralho monte { get; set; }

        public Jogador(string nome)
        {
            this.Nome = nome;
            this.ranking = new Fila(5);
            this.qtdCartasdoMonte = 0;
            this.posicao= 0;
            this.monte = new Baralho();
        }
        public void AdcionarRanking()
        {
            if (ranking.IsQueueIsFull())
            {
                ranking.Remover();
            }
            ranking.Inserir(posicao);
        }
    }
}
