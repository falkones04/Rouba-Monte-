using System.Reflection;

namespace Rouba_Monte
{
    internal class Jogador
    {
        private string nome;
        private int qtdDeCartasUlt;
        public Queue<int> ranking;
        private int pos;
        public Stack<Carta> monte;

        public Jogador(string nome)
        {
            this.nome = nome;
            qtdDeCartasUlt = 0;
            ranking = new Queue<int>(5);
            pos = 0;
            monte = new Stack<Carta>();
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public int QtdDeCartasUlt
        {
            get { return qtdDeCartasUlt; }
            set { qtdDeCartasUlt = value; }
        }
        public int Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        public Queue<int> GetRanking()
        {
            return ranking;
        }
        public void AdicionarRanking(int pos)
        {
            if (ranking.Count==5)
            {
                ranking.Dequeue(); 
            }
            ranking.Enqueue(pos);
            this.pos = pos;
        }
    }
}
