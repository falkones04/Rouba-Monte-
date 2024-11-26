using System;

namespace Jogo
{
    internal class Jogador
    {
        public string Nome { get; set; }
        private Fila ranking;
        public int Posicao { get; set; }
        public Baralho monte; 
        public int Quantidade { get; set; } 

        public Jogador(string nome)
        {
            Nome = nome;
            ranking = new Fila(5); 
            Posicao = 0;
            monte = new Baralho(); 
            Quantidade = 0;
        }
        public void AdicionarRanking()
        {
            if (ranking.IsQueueIsFull()) 
            {
                ranking.Remover(); 
            }
            ranking.Inserir(Posicao); 
        }

    }
}
