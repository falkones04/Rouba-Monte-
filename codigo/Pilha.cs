
namespace Jogo
{
    class Pilha
    {
        private Celula? topo;
        public Pilha()
        {
            topo = null;
        }
        public void Push(Carta x)
        {
            Celula? tmp = new Celula(x);
            tmp.Prox = topo;
            topo = tmp;
            tmp = null;
        }
        public Carta Pop()
        {
            if (topo == null)
                throw new Exception("Erro!!!");
            Carta? elem = topo.Elem;
            Celula? tmp = topo;
            topo = topo.Prox;
            tmp.Prox = null;
            tmp = null;
            return elem;
        }
        public void Mostrar()
        {
            for (Celula i = topo; i != null; i = i.Prox)
            {
                Console.WriteLine($"{i.Elem}");
            }
        }
        public Carta ExibirTopo()
        {
            return topo.Elem;
        }
    }
}
