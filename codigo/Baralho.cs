
namespace Jogo
{
    class Baralho : Pilha
    {
        private int numDeBaralho;

        public Baralho(int numDeBaralhos) : base()
        {
            if (numDeBaralhos <= 0)
                throw new Exception("Insira o um numero de baralhos maior que zero");
            this.numDeBaralho = numDeBaralhos;
            IniciarBaralho();
        }

        private void IniciarBaralho() 
        {
            string[] naipes = { "♠", "♣", "♥", "♦" };
            for (int i = 0; i < numDeBaralho; i++) 
            {
                foreach (string naipe in naipes) 
                {
                    for (int j = 1; j <= 13; j++)
                    {
                        Push(new Carta(j, naipe));
                    }
                }
            }
        }
    }


}
