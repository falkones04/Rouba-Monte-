﻿using System;
using System.Collections.Generic;
using System.Reflection.Metadata;


namespace Rouba_Monte
{
    class Program
    {
        static void Painel()
        {
            Console.WriteLine("==============================");
            Console.WriteLine("Rouba Montes \n");
            Console.WriteLine("1 - Iniciar partida");
            Console.WriteLine("2 - Ranking");
            Console.WriteLine("3 - Sair");
            Console.WriteLine("==============================");
        }
        static void Main(string[] args)
        {
            Jogo mesa1 = null;
            Fila jogador = null;
            int op = -1;
            while (op != 3)
            {
                Painel();
                op = int.Parse(Console.ReadLine());
                switch (op)
                {
                    case 1:
                    while(true){
                        if (jogador == null)
                        {
                            Console.WriteLine("Insira o numero de cartas");
                            int numCartas = int.Parse(Console.ReadLine());
                            Console.WriteLine("Insira o numero de jogadores");
                            int numJogador = int.Parse(Console.ReadLine());
                            mesa1 = new Jogo(numCartas, numJogador);
                            jogador = mesa1.ColocarPlayer(numJogador);
                            mesa1.RealizarPartida();
                            Console.WriteLine("Deseja jogar novamente? S ou N");
                                if (Console.ReadLine()?.ToUpper().Trim() == "S")
                                {
                                    continue;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        else{
                            Console.WriteLine("Insira o numero de cartas");
                            int numCartas = int.Parse(Console.ReadLine());
                            mesa1 = new Jogo(numCartas, jogador);
                            mesa1.RealizarPartida();
                            Console.WriteLine("Deseja jogar novamente? S ou N");
                                if(Console.ReadLine()?.ToUpper().Trim() == "S")
                                { 
                                    continue;
                                }
                                else
                                {
                                    break;
                                }
                            break;
                        }
                    }
                        break;
                    case 2:
                        string nome = Console.ReadLine();
                        foreach (var jogador1 in mesa1.jogadores)
                        {
                            if(jogador1.Nome==nome)
                            {
                                Console.Write($"Ranking {nome}:\n");
                                int[] temp = jogador1.ranking.ToArray();
                                for(int i =0; i<temp.Length;i++){
                                     Console.Write($"{temp[i]} ");
                                }
                                Console.Write("\n");
                            }
                        }
                        break;
                    case 3:
                        Console.WriteLine("Fim");
                        break;
                }
            }
        }
    }
}