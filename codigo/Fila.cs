namespace Jogo
{
    internal class Fila
    {
        private int[] array;
        private int primeiro, ultimo;

        public Fila(int tam) 
        {
            array = new int[tam];
             primeiro = ultimo = 0;
        }
        public void Inserir(int x)
        {
            if (((ultimo + 1) % array.Length) == primeiro)
                throw new Exception("Erro!");
            array[ultimo] = x;
            ultimo = (ultimo + 1) % array.Length;
        }
        public int Remover()
        {
            if (primeiro == ultimo)
                throw new Exception("Erro!");
            int resp = array[primeiro];
            primeiro = (primeiro + 1) % array.Length;
            return resp;
        }
        public bool IsQueueIsFull() 
        {
          return primeiro == ultimo;
        }
    }
}
