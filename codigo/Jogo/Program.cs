using System;

namespace Jogo
{
    class Rouba_Monte
    {
        static void Main(string[] args)
        {
            Carta carta1 = new Carta(1, "clover");
            Carta carta2 = new Carta(1, "spade");
            Carta carta3 = new Carta(1, "Heart");
            Carta carta4 = new Carta(1, "club");
            Pilha pilha = new Pilha();
            pilha.Push(carta1);
            pilha.Push(carta2);
            pilha.Push(carta3);
            pilha.Push(carta4);
            pilha.Mostrar();
        }
    }
}