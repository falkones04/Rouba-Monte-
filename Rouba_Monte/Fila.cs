using System;
using System.Collections;
using System.Collections.Generic;

namespace Rouba_Monte
{
    internal class Fila : IEnumerable<Jogador>
    {
        private Jogador[] array;
        private int primeiro, ultimo, atual;

        public Fila(int tam)
        {
            array = new Jogador[tam + 1];
            primeiro = ultimo = atual = 0;
        }

        public void Inserir(Jogador x)
        {
            if (((ultimo + 1) % array.Length) == primeiro)
                throw new Exception("Erro! A fila está cheia.");
            array[ultimo] = x;
            ultimo = (ultimo + 1) % array.Length;
        }

        public Jogador Remover()
        {
            if (primeiro == ultimo)
                throw new Exception("Erro! A fila está vazia.");
            Jogador resp = array[primeiro];
            primeiro = (primeiro + 1) % array.Length;
            return resp;
        }
        public Jogador JogadorAtual()
        {
            if (primeiro == ultimo)
                throw new Exception("Erro! A fila está vazia.");
            Jogador resp = array[atual];
            return resp;
        }
        public void ProximoJogador()
        {
            atual = (atual + 1) % array.Length;
            if(array[atual] == null)
            {
                atual = (atual + 1) % array.Length;
            }
        }

        public bool IsQueueIsFull()
        {
            return ((ultimo + 1) % array.Length) == primeiro;
        }

        // Implementando o GetEnumerator para que o foreach funcione
        public IEnumerator<Jogador> GetEnumerator()
        {
            int index = primeiro;
            while (index != ultimo)
            {
                yield return array[index];
                index = (index + 1) % array.Length;
            }
        }

        // Necessário para a compatibilidade com tipos não genéricos
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
