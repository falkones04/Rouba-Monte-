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
        public Jogador PrimeiroJogador(string nome)
        {
            if (primeiro == ultimo)
                throw new Exception("Erro! A fila está vazia.");
            Jogador resp = null;
            for(int i = 0;i!=ultimo;i++)
            {
            if (nome==array[i].Nome)
            {
                resp = array[i];
            }
            }
            return resp;
        }
        public Jogador ProximoJogador()
        {
            atual = (atual + 1) % array.Length;
            if(array[atual] == null)
            {
                atual = (atual + 1) % array.Length;
                return array[atual];
            }
        return array[atual];
        }

        public bool IsQueueIsFull()
        {
            return ((ultimo + 1) % array.Length) == primeiro;
        }
        public IEnumerator<Jogador> GetEnumerator()
        {
            int index = primeiro;
            while (index != ultimo)
            {
                yield return array[index];
                index = (index + 1) % array.Length;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    
    }


}
