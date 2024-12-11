using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Rouba_Monte
{
    internal class Fila : IEnumerable<Jogador>
    {
        public Jogador[] array;
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
        public Jogador PrimeiroJogador(string nome)
        {
            if (primeiro == ultimo)
                throw new Exception("Erro! A fila está vazia.");
            for (int i = primeiro; i != ultimo; i = (i + 1) % array.Length)
            {
                if (nome == array[i].Nome)
                {
                    return array[i];
                }
            }
            return null;
        }
        public Jogador ProximoJogador()
        {
            atual = (atual + 1) % array.Length;
            if (array[atual] == null)
            {
                atual = (atual + 1) % array.Length;
                return array[atual];
            }
            return array[atual];
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

        public Jogador[] Ordenar()
        {
            int count = array.Length - 1;
            Jogador[] temp = new Jogador[count];
            for (int i = primeiro; i <ultimo; i++)
                temp[i] = array[i];
            QuickSort(temp, 0, count - 1);
            return temp;
        }
        private void QuickSort(Jogador[] array, int esq, int dir)
        {
            int pivo = array[(dir + esq) / 2].QtdDeCartasUlt;
            int j = dir, i = esq;
            while (i <= j)
            {
                while (array[i].QtdDeCartasUlt > pivo)
                    i++;
                while (array[j].QtdDeCartasUlt < pivo)
                    j--;
                if (i<=j)
                {
                    Jogador temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    j--;
                    i++;
                }
            }
            if (esq < j)
            {
                QuickSort(array, esq, j);
            }
            if (i < dir)
            {
                QuickSort(array, i, dir);
            }
        }
    }
}
