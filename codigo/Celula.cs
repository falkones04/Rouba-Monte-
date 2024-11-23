
namespace Jogo
{
    class Celula
    {
        private Carta elem;
        private Celula prox;

        public Celula()
        {
            this.elem = null;
            this.prox = null;
        }
        public Celula(Carta elem)
        {
            this.elem = elem;
            this.prox = null;
        }
        public Carta Elem
        {
            get { return this.elem; }
            set { this.elem = value; }
        }
        public Celula Prox
        {
            get { return this.prox; }
            set { this.prox = value; }
        }

    }
}
