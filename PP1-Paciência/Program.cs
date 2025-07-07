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
                                const int COL_WIDTH = 18;
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
                                    Console.WriteLine("\n");
                                }
                                else
                                {
                                    Carta cartaMonte = paciencia.ObterMonte().MostrarTopo();
                                    Console.WriteLine($"[Carta do monte: {cartaMonte.GetValor()}{cartaMonte.GetNaipe()}]");
                                    
                                }
                                foreach (var fundacao in paciencia.ObterFundacoes())
                                {
                                    var topo = fundacao.MostrarTopo();
                                    Console.Write(topo != null ? $"| [{topo.GetValor()}{topo.GetNaipe()}] |".PadRight(COL_WIDTH) : "| [  ] |".PadRight(10));
                                }
                                Console.WriteLine("\n");
                                var pilhas = paciencia.ObterPilhas();
                                int numPilhas = pilhas.Count;
                                int maxAltura = pilhas.Max(p => p.ContarCartas());

                                for (int p = 0; p < numPilhas; p++)
                                {
                                    string header = $"P{p + 1}";
                                    Console.Write(header.PadRight(COL_WIDTH));
                                    Console.Write("| ");
                                }
                                Console.WriteLine();

                                for (int linha = 0; linha < maxAltura; linha++)
                                {
                                    for (int p = 0; p < numPilhas; p++)
                                    {
                                        var cartasPilha = pilhas[p].ObterCartas();
                                        string texto;

                                        if (linha < cartasPilha.Count)
                                        {
                                            var carta = cartasPilha[linha];
                                            texto = carta.Virada
                                                ? $"{carta.GetValor()}{carta.GetNaipe()}"
                                                : "---";
                                        }
                                        else
                                        {
                                            texto = "";
                                        }

                                        
                                        Console.Write(texto.PadRight(COL_WIDTH));
                                        Console.Write("| ");
                                    }

                                    Console.WriteLine();
                                }
                                Console.WriteLine("== MENU DE OPÇÕES ==");
                                Console.WriteLine("1 - Mover uma Carta para outra Pilha");
                                Console.WriteLine("2 - Mover uma carta do Monte para Pilha");
                                Console.WriteLine("3 - Mover uma carta do Monte para a Fundação");
                                Console.WriteLine("4 - Mover uma carta para a Fundação");
                                Console.WriteLine("5 - Comprar do Baralho");
                                Console.WriteLine("6 - Voltar o Baralho");
                                Console.WriteLine("7 - Sair da Partida");
                                int opcaoJogo = int.Parse(Console.ReadLine());
                                if (opcaoJogo < 1 || opcaoJogo > 7)
                                {
                                    Console.WriteLine("Opção inválida. Tente novamente.");
                                    continue;
                                }
                                else if (opcaoJogo == 7)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Saindo da partida...");
                                    break;
                                }
                                else if (opcaoJogo == 1)
                                {
                                    Console.WriteLine("Insira a Posição da Pilha de Origem");
                                    int pilhaOrigem = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Insira a Posição da Pilha de Destino");
                                    int pilhaDestino = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Quantas Cartas Quer Mover?");
                                    int range = int.Parse(Console.ReadLine());
                                    if (range < 1 || range > paciencia.ObterPilhas()[pilhaOrigem - 1].ContarCartas())
                                    {
                                        Console.WriteLine("Quantidade inválida. Tente novamente.");
                                        continue;
                                    }
                                    else if(range == 1)
                                    {
                                        bool movimentoValido = paciencia.MoverCartaPilha(pilhaOrigem, pilhaDestino);
                                        if (movimentoValido)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Movimento realizado com sucesso!");
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Movimento inválido. Tente novamente.");
                                        }
                                    }
                                    else
                                    {
                                        bool movimentoValido = paciencia.MoverCartaPilha(pilhaOrigem, pilhaDestino, range);
                                        if(movimentoValido)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Movimento realizado com sucesso!");
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Movimento inválido. Tente novamente.");
                                        }
                                    }  
                                }
                                else if(opcaoJogo == 2) 
                                {
                                    Console.WriteLine("insira a Posição da Pilha de Destino");
                                    int pilhaDestino = int.Parse(Console.ReadLine());
                                    bool movimentoValido = paciencia.PegarCartaMonte(pilhaDestino);
                                    if (movimentoValido)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Carta movida do monte para a pilha com sucesso!");
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Movimento inválido. Tente novamente.");
                                    }
                                }
                                else if(opcaoJogo == 3)
                                {
                                    Console.WriteLine("Insira a Posição da Fundação de Destino");
                                    int fundacaoDestino = int.Parse(Console.ReadLine());
                                    bool movimentoValido = paciencia.PegarCartaMonteFundacao(fundacaoDestino);
                                    if (movimentoValido)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Carta movida do monte para a fundação com sucesso!");
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Movimento inválido. Tente novamente.");
                                    }
                                }
                                else if (opcaoJogo == 4)
                                {
                                    Console.WriteLine("Insira a Posição da Pilha de Origem");
                                    int pilhaDestino = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Insira a Posição da Fundação de Destino");
                                    int fundacaoDestino = int.Parse(Console.ReadLine());
                                    bool movimentoValido = paciencia.MoverCartaFundacao(pilhaDestino, fundacaoDestino);
                                    if (movimentoValido)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Carta movida para a fundação com sucesso!");
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Movimento inválido. Tente novamente.");
                                    }
                                }
                                else if (opcaoJogo == 5)
                                {
                                    Console.Clear();
                                    paciencia.PegarCartaBaralho();
                                }
                                else if (opcaoJogo == 6)
                                {
                                    Console.Clear();
                                    paciencia.VoltarBaralho();
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
