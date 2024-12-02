using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Rouba_Monte
{
    internal class Jogador
    {
        private string nome;
        private int qtdDeCartasUlt;
        private List<int> ranking;
        private int pos;
        public Stack<Carta> monte; 

        public Jogador(string nome)
        {
            this.nome = nome;
            qtdDeCartasUlt = 0;
            ranking = new List<int>(5);
            pos = 0;
            monte = new Stack<Carta>();
        }
        public string Nome 
            {
            get { return nome; }
            set { nome = value;}
            }
    }
}
