using System;
using System.Collections.Generic;


namespace Rouba_Monte
{
    class Program 
    {
        static void Main(string[] args)
        {
            Jogo mesa1 = new Jogo(52, 3);
            mesa1.ExibirBaralho();
            mesa1.RealizarJogada();

        }
    }

}
