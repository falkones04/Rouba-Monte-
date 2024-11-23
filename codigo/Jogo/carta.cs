

namespace Jogo
{
    class Carta
    {
        private int num;
        private string naipe;

        public Carta(int num, string naipe)
        {
            this.num = num;
            this.naipe = naipe;
        }
        public int GetNum() { return num; }

        public string GetNaipe() { return naipe; }

        public override string ToString()
        {
            return $"{num} de {naipe}";
        }
    }
}

