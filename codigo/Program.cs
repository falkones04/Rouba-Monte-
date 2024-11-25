using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
namespace Jogo
{
    class Rouba_Monte
    {
        static void Main(string[] args)
        {
            Baralho n1 = new Baralho();
            Baralho n2 = new Baralho();
            Jogador p1 = new Jogador("anderson");

            p1.PegarOMonte(p1.monte,n2);
            p1.monte.Mostrar();
        }
    }
}