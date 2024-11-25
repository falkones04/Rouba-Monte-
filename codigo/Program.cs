using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
namespace Jogo
{
    class Rouba_Monte
    {
        static void Main(string[] args)
        {
            Mesa teste  = new Mesa(2,1);
            teste.ColocarPlayer(3);
            teste.ComprarCartaDaVez();
        }
    }
}