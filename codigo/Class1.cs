using System;
using System.Collections.Generic;

namespace Jogo
{
    internal class Mesa
    {
        public Queue<Jogador> Jogadores;
        public List<Carta> areaDeDescarte;
        public Baralho Monte;
        public int numDeJogador;
        public Mesa(int numDeJogador, int numDeBaralhos)
        {
            Jogadores = new Queue<Jogador>(numDeJogador);
            areaDeDescarte = new List<Carta>();
            Monte = new Baralho(numDeBaralhos);
            this.numDeJogador = numDeJogador;
            ColocarPlayer(numDeJogador);
        }
        public void ColocarPlayer(int numdeJogador)
        {
            for (int i = 0; i < numdeJogador; i++)
            {
                Console.WriteLine($"Insira o nome do jogador {i + 1}");
                Jogador J = new Jogador(Console.ReadLine());
                Jogadores.Enqueue(J);
            }
            foreach (var jogador in Jogadores) { Console.WriteLine(jogador.Nome); }
        }
        public Carta ComprarCartaDaVez()
        {
            if(Monte.quantidade == 0)
            {
                Console.WriteLine("Monte vazio");
                return null;
            }
            Carta cartaDaVez = Monte.Pop();
            return cartaDaVez;
        }
        public void RealizarJogada(Jogador jogadorAtual)
        {
            bool continuarJogando = false;

            while (true)
            {
                Carta cartaDaVez = ComprarCartaDaVez();
                if (cartaDaVez == null)
                {
                    Console.WriteLine("Fim do jogo: não há mais cartas no monte.");
                    return;
                }
                Console.WriteLine($"{jogadorAtual.Nome} comprou a carta: {cartaDaVez}");
                continuarJogando = Acao1(jogadorAtual, cartaDaVez);
                if (continuarJogando)
                {
                    Console.WriteLine($"Ação 1 executada com sucesso e o monte tem {jogadorAtual.Quantidade}.");
                    continue;
                }

                continuarJogando = Acao2(jogadorAtual, cartaDaVez);
                if (continuarJogando)
                {
                    Console.WriteLine($"Ação 2 executada com sucesso e o monte tem {jogadorAtual.Quantidade}.");
                    continue;
                }

                continuarJogando = Acao3(jogadorAtual, cartaDaVez);
                if (continuarJogando)
                {
                    Console.WriteLine($"Ação 3 executada com sucesso e o monte tem {jogadorAtual.Quantidade}");
                    continue;
                }
                areaDeDescarte.Add(cartaDaVez);
                Console.WriteLine($"{jogadorAtual.Nome} colocou a carta {cartaDaVez} na área de descarte.");
                break; 
            }
        }

        public bool Acao1(Jogador jogadorAtual,Carta cartaDaVez)
        {
            int maiorQtd = 0;
            Jogador jogadorParaRoubar= null;
            Random rand = new Random(); 
            foreach (var jogador in Jogadores)
            {
                if (jogador != jogadorAtual && jogador.Quantidade > 0)
                {
                    Carta topoMonte = jogador.monte.ExibirTopo();
                    if (topoMonte != null && cartaDaVez.GetNum() == topoMonte.GetNum())
                    {
                        if (jogador.Quantidade > maiorQtd)
                        {
                            maiorQtd = jogador.Quantidade;
                            jogadorParaRoubar = jogador;
                        }

                        else if (jogador.Quantidade == maiorQtd)
                        {
                          
                            if (rand.Next(2) == 0)
                            {
                                jogadorParaRoubar = jogador;
                            }
                        }
                    }
                }
            }
            if (jogadorParaRoubar != null)
            {
                Console.WriteLine($"{jogadorAtual.Nome} roubou o monte de {jogadorParaRoubar.Nome}!");
                PegarOMonte(jogadorAtual, jogadorParaRoubar);
                jogadorAtual.monte.Push(cartaDaVez);
                jogadorAtual.Quantidade++;
                return true;
            }
            return false;
        }
        public bool Acao2(Jogador jogadorAtual, Carta cartaDaVez)
        {
            Carta cartaIgualDescarte = CartaDescarteIgual(cartaDaVez);
            if (cartaIgualDescarte != null)
            {
                areaDeDescarte.Remove(cartaIgualDescarte);
                jogadorAtual.monte.Push(cartaIgualDescarte);
                jogadorAtual.monte.Push(cartaDaVez);
                jogadorAtual.Quantidade += 2;
                Console.WriteLine($"{jogadorAtual.Nome} pegou a carta {cartaIgualDescarte} da area de descarte! e o Monte tem {jogadorAtual.Quantidade} cartas");
                return true;
            }
            return false;
        }
        public bool Acao3(Jogador jogadorAtual, Carta cartaDaVez)
        {
            if (jogadorAtual.Quantidade > 0)
            {
                Carta topoMeuMonte = jogadorAtual.monte.ExibirTopo();
                if (topoMeuMonte != null && cartaDaVez.GetNum() == topoMeuMonte.GetNum()) 
                {
                    Console.WriteLine($"{jogadorAtual.Nome} colocou a carta no topo do próprio monte!");
                    jogadorAtual.monte.Push(cartaDaVez);
                    jogadorAtual.Quantidade++;
                    Console.WriteLine($"O jogador {jogadorAtual.Nome} agora tem {jogadorAtual.Quantidade} cartas.");
                    return true;
                }
            }
            return false;
        }
        public Carta CartaDescarteIgual(Carta cartaDaVez)
        {
            foreach (var carta in areaDeDescarte)
            {
                if (cartaDaVez.GetNum() == carta.GetNum())
                { return carta; }

            }
            return null;
        }
        public void PegarOMonte(Jogador jogadorAtual, Jogador jogadorParaRoubar)
        {
            Pilha tmp = new Pilha();
            for (Celula i = jogadorParaRoubar.monte.Topo; i != null; i = i.Prox)
            {
                Console.WriteLine($"Carta {i.Elem} será transferida do monte de {jogadorParaRoubar.Nome}");
                tmp.Push(jogadorParaRoubar.monte.Pop());
                jogadorParaRoubar.Quantidade--;
            }
            for (Celula i = tmp.Topo; i != null; i = i.Prox)
            {
                jogadorAtual.monte.Push(tmp.Pop());
                jogadorAtual.Quantidade++;
            }
        }
    }
}
