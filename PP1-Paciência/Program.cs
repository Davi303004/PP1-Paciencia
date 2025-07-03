using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PP1_Paciência.Cartas;

namespace PP1_Paciência
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) 
            {
                try 
                {
                    Console.WriteLine("==PACIÊNCIA==");
                    Console.WriteLine("1 - Iniciar Jogo");
                    Console.WriteLine("0 - Sair");
                    int opcao = int.Parse(Console.ReadLine());
                    if(opcao < 0 || opcao > 1) 
                    {
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        continue;
                    }
                    if (opcao == 0)
                    {
                        Console.WriteLine("Saindo do jogo...");
                        break;
                    }
                    else
                    {
                        Jogo paciencia = new Jogo();
                        paciencia.IniciarJogo();
                        while (paciencia.VerficarVitoria() == false)
                        {
                            try 
                            {
                                Console.WriteLine("BARALHO");
                                if (paciencia.ContarCartasBaralho() == 0)
                                {
                                    Console.WriteLine("[  ]");
                                    Console.WriteLine("\t");
                                }
                                else
                                {
                                    Console.WriteLine($"[Cartas restantes no baralho: {paciencia.ContarCartasBaralho()}]");
                                    Console.WriteLine("\t");
                                }
                                Console.WriteLine("MONTE DE COMPRAS");
                                if (paciencia.ObterMonte().EstaVazia())
                                {
                                    Console.WriteLine("[  ]");
                                    Console.WriteLine("\t");
                                }
                                else
                                {
                                    Carta cartaMonte = paciencia.ObterMonte().MostrarTopo();
                                    Console.WriteLine($"[Carta do monte: {cartaMonte.GetValor()} de {cartaMonte.GetNaipe()}]");
                                    Console.WriteLine("\t");
                                }
                                foreach (var fundacao in paciencia.ObterFundacoes())
                                {
                                    var topo = fundacao.MostrarTopo();
                                    Console.Write(topo != null ? $"{topo.GetValor()}{topo.GetNaipe()}".PadRight(10) : "[  ]".PadRight(10));
                                }
                                Console.WriteLine("\n");
                                int maxAltura = paciencia.ObterPilhas().Max(p => p.ContarCartas());
                                for (int i = 0; i < maxAltura; i++)
                                {
                                    for (int j = 0; j < paciencia.ObterPilhas().Count; j++)
                                    {
                                        if (i < paciencia.ObterPilhas()[j].ContarCartas())
                                        {
                                            var cartasPilha = paciencia.ObterPilhas()[j].ObterCartas();
                                            Carta carta = cartasPilha.ElementAtOrDefault(i);
                                            if (carta == null)
                                            {
                                                Console.Write("\t");
                                                continue;
                                            }
                                            else if (carta.Virada) 
                                            {
                                                string textoCarta = ($"{carta.GetValor()} de {carta.GetNaipe()}");
                                                Console.Write(textoCarta + "\t");
                                            }
                                            else
                                            {
                                                Console.Write(" --- \t");
                                            }
                                        }
                                        else
                                        {
                                            Console.Write(" \t");
                                        }
                                    }
                                    Console.WriteLine();
                                }
                                
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Erro: {ex.Message}");
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }
    }
}
