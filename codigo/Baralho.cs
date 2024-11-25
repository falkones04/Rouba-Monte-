namespace Jogo
{
    class Baralho : Pilha
    {
        private int numDeBaralho;
        private Random random = new Random();

        public Baralho(int numDeBaralhos) : base()
        {
            if (numDeBaralhos <= 0)
                throw new ArgumentException("Insira um número de baralhos maior que zero");

            this.numDeBaralho = numDeBaralhos;
            IniciarBaralho();
        }
        public Baralho() : base()
        {
            Pilha pilha = new Pilha();
        }
        private void IniciarBaralho()
        {
            List<Carta> list = new List<Carta>();
            string[] naipes = { "♠", "♣", "♥", "♦" };
            for (int i = 0; i < numDeBaralho; i++)
            {
                foreach (string naipe in naipes)
                {
                    for (int j = 1; j <= 13; j++)
                    {
                        list.Add(new Carta(j, naipe));
                    }
                }
            }
            Shuffle(list);
            foreach (var carta in list)
            {
                Push(carta);
            }
        }

        private void Shuffle(List<Carta> list)  //FisherYatesShuffle
        {
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