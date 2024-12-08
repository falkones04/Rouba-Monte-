using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Rouba_Monte
{
    class Jogo
    {
        public Stack<Carta> baralho { get; set; }
        public Fila jogadores;
        public List<Carta> areaDeDescarte;

        public Jogo(int numDeCartas, Fila jogadores)
        {
            baralho = new Stack<Carta>(numDeCartas);
            this.jogadores = jogadores;
            areaDeDescarte = new List<Carta>();
            int numDeBaralhos = numDeCartas / 52;
            int numDeExcedentes = numDeCartas % 52;

            IniciarBaralho(numDeBaralhos, numDeExcedentes);
        }
        public Jogo(int numDeCartas, int numDeJogador)
        {
            baralho = new Stack<Carta>(numDeCartas);
            jogadores = new Fila(numDeJogador);
            areaDeDescarte = new List<Carta>();
            int numDeBaralhos = numDeCartas / 52;
            int numDeExcedentes = numDeCartas % 52;

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

        private void Shuffle(List<Carta> list)
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
        public void RealizarPartida()
        {
            bool continuarJogando = false;
            Console.WriteLine("Escreva o nome do primeiro jogador");
            string nome = Console.ReadLine();
            Jogador jogadorAtual = jogadores.PrimeiroJogador(nome);
            int round = 1;
            string barra = $"============================================================";
            RegistrarLog(barra);
            while (true)
            {
                Carta cartaDaVez = ComprarCartaDaVez();
                {
                    if (cartaDaVez != null)
                    {
                        Console.WriteLine($"Round: {round}");
                        string round1 = $"Round: {round.ToString()}";
                        RegistrarLog(round1);
                        Console.WriteLine($"{jogadorAtual.Nome} comprou a carta: {cartaDaVez}");
                        string frase = $"{jogadorAtual.Nome} comprou a carta: {cartaDaVez}";
                        RegistrarLog(frase);

                        continuarJogando = Roubar(jogadorAtual, cartaDaVez);
                        if (continuarJogando)
                        {
                            round++;
                            continue;
                        }

                        continuarJogando = BuscarDescarte(jogadorAtual, cartaDaVez);
                        if (continuarJogando)
                        {
                            round++;
                            continue;
                        }

                        continuarJogando = Colocar(jogadorAtual, cartaDaVez);
                        if (continuarJogando)
                        {
                            round++;
                            continue;
                        }
                        areaDeDescarte.Add(cartaDaVez);
                        Console.WriteLine($"{jogadorAtual.Nome} colocou a carta {cartaDaVez} na área de descarte.");
                        jogadorAtual = jogadores.ProximoJogador();
                        round++;
                    }
                    else
                    {
                        //ranking compara o tamanho dos montes e retorna eles ordenados pelo tamanho do monte, EXIBIR NO FINAL DO JOGO, faz variavel pra colocar o trem
                        //pipipi popopo e lembra de no final joga essa variavel temp q recebe o log pra outra do arquivo que recebe esse log agr, sim é feito e é repetição
                        Console.WriteLine("Fim De Jogo");
                        RegistrarLog(barra);
                        break;
                    }
                }

            }
        }
        private void RegistrarVencedor()
        {

        }
        private void RegistrarLog(string x)
        {
            string path = @"C:/Users/Samuel/Documents/GitHub/Rouba-Monte-/Rouba_Monte/log.txt";
            try
            {
                using (StreamWriter frase = new StreamWriter(path, append:true))
                {
                    frase.WriteLine(x + "\n");
                }

            }
            catch
            {
                throw new Exception("Erro!");
            } 
        }
        public Fila ColocarPlayer(int numdeJogador)
        {
            for (int i = 0; i < numdeJogador; i++)
            {
                Console.WriteLine($"Insira o nome do jogador {i + 1}");
                Jogador j = new Jogador(Console.ReadLine());
                jogadores.Inserir(j);
            }
            return jogadores;
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
        private bool Roubar(Jogador jogadorAtual, Carta cartaDaVez)
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
                string frase = $"{jogadorAtual.Nome} roubou o monte de {jogadorParaRoubar.Nome}!";
                RegistrarLog(frase);
                PegarOMonte(jogadorAtual, jogadorParaRoubar);
                jogadorAtual.monte.Push(cartaDaVez);
                return true;
            }
            return false;
        }
        private bool BuscarDescarte(Jogador jogadorAtual, Carta cartaDaVez)
        {
            Carta cartaIgualDescarte = CartaDescarteIgual(cartaDaVez);
            if (cartaIgualDescarte != null)
            {
                areaDeDescarte.Remove(cartaIgualDescarte);
                jogadorAtual.monte.Push(cartaIgualDescarte);
                jogadorAtual.monte.Push(cartaDaVez);
                Console.WriteLine($"{jogadorAtual.Nome} pegou a carta {cartaIgualDescarte} da area de descarte! e o Monte tem {jogadorAtual.monte.Count} cartas");
                string frase = $"{jogadorAtual.Nome} pegou a carta {cartaIgualDescarte} da area de descarte! e o Monte tem {jogadorAtual.monte.Count} cartas";
                RegistrarLog(frase);
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool Colocar(Jogador jogadorAtual, Carta cartaDaVez)
        {
            if (jogadorAtual.monte.Count > 0)
            {
                Carta topoMeuMonte = jogadorAtual.monte.Peek();
                if (topoMeuMonte != null && cartaDaVez.GetNum() == topoMeuMonte.GetNum())
                {
                    Console.WriteLine($"{jogadorAtual.Nome} colocou a carta no topo do próprio monte!");
                    string frase = $"{jogadorAtual.Nome} colocou a carta no topo do próprio monte!";
                    RegistrarLog(frase);
                    jogadorAtual.monte.Push(cartaDaVez);
                    Console.WriteLine($"O jogador {jogadorAtual.Nome} agora tem {jogadorAtual.monte.Count} cartas.");
                    frase = $"O jogador {jogadorAtual.Nome} agora tem {jogadorAtual.monte.Count} cartas.";
                    RegistrarLog(frase);
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
