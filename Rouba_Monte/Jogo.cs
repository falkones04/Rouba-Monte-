using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Rouba_Monte
{
    class Jogo
    {
        public Stack<Carta> baralho { get; set; }
        public Fila jogadores;
        public List<Carta> areaDeDescarte;

        public Jogo(int numDeCartas, int numDeJogador)
        {
            baralho = new Stack<Carta>(numDeCartas);
            jogadores = new Fila(numDeJogador);
            areaDeDescarte = new List<Carta>();
            int numDeBaralhos = numDeCartas / 52;
            int numDeExcedentes = numDeCartas % 52;

            ColocarPlayer(numDeJogador);
            IniciarBaralho(numDeBaralhos, numDeExcedentes);

        }
        private void IniciarBaralho(int numBaralhos, int NumExcedentes)
        {
            List<Carta> list = new List<Carta>();
            string[] naipes = { "♠", "♣", "♥", "♦" };
            foreach (string naipe in naipes)
            {
                for (int valor = 1; valor <= 13; valor++)
                {
                    list.Add(new Carta(valor, naipe));
                }
            }
            if (numBaralhos > 0)
            {
                for (int i = 0; i < numBaralhos; i++)
                {
                    Shuffle(list);
                    foreach (var carta in list)
                    {
                        baralho.Push(carta);
                    }
                }
            }
            else if (NumExcedentes != 0)
            {
                Shuffle(list);
                foreach (var carta in list.Slice(0, NumExcedentes))
                {
                    baralho.Push(carta);
                }
            }
            Console.WriteLine("Baralho Criado Com Sucesso");
        }

        private void Shuffle(List<Carta> list) // Fisher-Yates Shuffle
        {
            Random random = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int randomIndex = random.Next(i + 1);
                Carta temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }
        public void ExibirBaralho()
        {
            if (baralho.Count == 0)
            {
                Console.WriteLine("O baralho está vazio.");
                return;
            }

            Console.WriteLine("Cartas no baralho:");
            foreach (var carta in baralho)
            {
                Console.WriteLine($"{carta}");
            }
        }
        public void RealizarJogada()
        {
            bool continuarJogando = false;
            Console.WriteLine("Escreva o nome do primeiro jogador");
            string nome = Console.ReadLine();
            Jogador jogadorAtual = jogadores.PrimeiroJogador(nome);
            while(true){
            Carta cartaDaVez = ComprarCartaDaVez();
                {
                    if (cartaDaVez != null)
                    {
                        Console.WriteLine($"{jogadorAtual.Nome} comprou a carta: {cartaDaVez}");
                        continuarJogando = Acao1(jogadorAtual, cartaDaVez);
                        if (continuarJogando)
                        {
                            continue;
                        }

                        continuarJogando = Acao2(jogadorAtual, cartaDaVez);
                        if (continuarJogando)
                        {
                            continue;
                        }

                        continuarJogando = Acao3(jogadorAtual, cartaDaVez);
                        if (continuarJogando)
                        {
                            continue;
                        }
                        areaDeDescarte.Add(cartaDaVez);
                        Console.WriteLine($"{jogadorAtual.Nome} colocou a carta {cartaDaVez} na área de descarte.");
                        jogadorAtual = jogadores.ProximoJogador();

                    }
                    else
                    {
                        Console.WriteLine("Fim De Jogo");
                        break;
                    }
                }

            }
        }
        private void ColocarPlayer(int numdeJogador)
        {
            for (int i = 0; i < numdeJogador; i++)
            {
                Console.WriteLine($"Insira o nome do jogador {i + 1}");
                Jogador j = new Jogador(Console.ReadLine());
                jogadores.Inserir(j);
            }
        }
        private Carta ComprarCartaDaVez()
        {
            if (baralho.Count == 0)
            {
                Console.WriteLine("Monte vazio");
                return null;
            }
            Carta cartaDaVez = baralho.Pop();
            return cartaDaVez;
        }
        private bool Acao1(Jogador jogadorAtual, Carta cartaDaVez)
        {
            int maiorQtd = 0;
            Jogador jogadorParaRoubar = null;
            Random rand = new Random();
            foreach (var jogador in jogadores)
            {
                if (jogador != jogadorAtual && jogador.monte.Count > 0)
                {
                    Carta topoMonte = jogador.monte.Peek();
                    if (topoMonte != null && cartaDaVez.GetNum() == topoMonte.GetNum())
                    {
                        if (jogador.monte.Count > maiorQtd)
                        {
                            maiorQtd = jogador.monte.Count;
                            jogadorParaRoubar = jogador;
                        }

                        else if (jogador.monte.Count == maiorQtd)
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
                return true;
            }
            return false;
        }
        private bool Acao2(Jogador jogadorAtual, Carta cartaDaVez)
        {
            Carta cartaIgualDescarte = CartaDescarteIgual(cartaDaVez);
            if (cartaIgualDescarte != null)
            {
                areaDeDescarte.Remove(cartaIgualDescarte);
                jogadorAtual.monte.Push(cartaIgualDescarte);
                jogadorAtual.monte.Push(cartaDaVez);
                Console.WriteLine($"{jogadorAtual.Nome} pegou a carta {cartaIgualDescarte} da area de descarte! e o Monte tem {jogadorAtual.monte.Count} cartas");
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool Acao3(Jogador jogadorAtual, Carta cartaDaVez)
        {
            if (jogadorAtual.monte.Count > 0)
            {
                Carta topoMeuMonte = jogadorAtual.monte.Peek();
                if (topoMeuMonte != null && cartaDaVez.GetNum() == topoMeuMonte.GetNum())
                {
                    Console.WriteLine($"{jogadorAtual.Nome} colocou a carta no topo do próprio monte!");
                    jogadorAtual.monte.Push(cartaDaVez);
                    Console.WriteLine($"O jogador {jogadorAtual.Nome} agora tem {jogadorAtual.monte.Count} cartas.");
                    return true;
                }
            }
            return false;
        }
        private void PegarOMonte(Jogador jogadorAtual, Jogador jogadorParaRoubar)
        {
            Stack<Carta> tmp = new Stack<Carta>();
            while (jogadorParaRoubar.monte.Count != 0)
            {
                Carta x = jogadorParaRoubar.monte.Pop();
                tmp.Push(x);
            }
            while (tmp.Count != 0)
            {
                Carta x = tmp.Pop();
                jogadorAtual.monte.Push(x);
            }
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

    }
}
