﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PP1_Paciência.Cartas;

namespace PP1_Paciência
{
    class Jogo
    {
        private Baralho baralho;
        private Pilha monte;
        private List<Pilha> pilhas;
        private List<Pilha> fundacoes;

        public void IniciarJogo()
        {
            baralho = new Baralho();
            baralho.Embaralhar();
            pilhas = new List<Pilha>();
            fundacoes = new List<Pilha>();
            monte = new Pilha();

            for (int i = 1; i <= 7; i++)
            {
                Pilha novaPilha = new Pilha();
                List<Carta> cartasDaPilha = baralho.SepararCartas(i);
                cartasDaPilha[cartasDaPilha.Count - 1].Virar();
                novaPilha.AdicionarCarta(cartasDaPilha);
                pilhas.Add(novaPilha);
            }

            for (int i = 0; i < 4; i++)
            {
                fundacoes.Add(new Pilha());
            }
        }
        public List<Pilha> ObterPilhas()
        {
            return pilhas;
        }
        public List<Pilha> ObterFundacoes()
        {
            return fundacoes;
        }
        public Pilha ObterMonte()
        {
            return monte;
        }
        public int ContarCartasBaralho()
        {
            return baralho.ObterCartas().Count;
        }
        private bool VerificarCor(Naipe n1, Naipe n2)
        {
            return (n1 == Naipe.Copas || n1 == Naipe.Ouros) && (n2 == Naipe.Paus || n2 == Naipe.Espadas) ||
                   (n1 == Naipe.Paus || n1 == Naipe.Espadas) && (n2 == Naipe.Copas || n2 == Naipe.Ouros);
        }
        public bool MoverCartaPilha(int pilhaOrigem, int pilhaDestino, int range) 
        {
            if (pilhaOrigem < 0 || pilhaOrigem > pilhas.Count || pilhaDestino < 0 || pilhaDestino > pilhas.Count || pilhaOrigem == pilhaDestino)
            {
                throw new ArgumentOutOfRangeException("Índices de pilha inválidos.");
            }
            if (pilhas[pilhaOrigem - 1].ContarCartas() < range)
            {
                throw new InvalidOperationException("A pilha de origem não possui cartas suficientes.");
            }
            List<Carta> cartasMovidas = pilhas[pilhaOrigem - 1].ObterCartas().GetRange(pilhas[pilhaOrigem - 1].ContarCartas() - range, range);
            if (pilhas[pilhaDestino - 1].EstaVazia())
            {
                if (cartasMovidas[0].GetValor() == Valor.Rei)
                {
                    pilhas[pilhaDestino - 1].AdicionarCarta(cartasMovidas);
                    foreach (Carta carta in cartasMovidas)
                    {
                        pilhas[pilhaOrigem - 1].RemoverCarta(); 
                    }
                    return true;
                }
                else
                {
                    int indiceTopo = pilhas[pilhaOrigem - 1].ContarCartas() - 1;
                    if (indiceTopo >= 0 && !pilhas[pilhaOrigem - 1].ObterCartas()[indiceTopo].Virada)
                    {
                        pilhas[pilhaOrigem - 1].ObterCartas()[indiceTopo].Virar();
                    }
                    pilhas[pilhaOrigem - 1].AdicionarCarta(cartasMovidas);
                    return false;
                }
            }
            Carta topoDestino = pilhas[pilhaDestino - 1].MostrarTopo();
            if (cartasMovidas[0].GetValor() == topoDestino.GetValor() - 1 && VerificarCor(cartasMovidas[0].GetNaipe(), topoDestino.GetNaipe()))
            {
                pilhas[pilhaDestino - 1].AdicionarCarta(cartasMovidas);
                foreach (Carta carta in cartasMovidas)
                {
                    pilhas[pilhaOrigem - 1].RemoverCarta();
                }
                return true;
            }
            else
            {
                int indiceTopo = pilhas[pilhaOrigem - 1].ContarCartas() - 1;
                if (indiceTopo >= 0 && !pilhas[pilhaOrigem - 1].ObterCartas()[indiceTopo].Virada)
                {
                    pilhas[pilhaOrigem - 1].ObterCartas()[indiceTopo].Virar();
                }
                pilhas[pilhaOrigem - 1].AdicionarCarta(cartasMovidas);
                return false;
            }
        }
        public bool MoverCartaPilha(int pilhaOrigem, int pilhaDestino)
        {
            if(pilhaOrigem < 0 || pilhaOrigem > pilhas.Count || pilhaDestino < 0 || pilhaDestino > pilhas.Count || pilhaOrigem == pilhaDestino)
            {
                throw new ArgumentOutOfRangeException("Índices de pilha inválidos.");
            }
            if (pilhas[pilhaOrigem - 1].EstaVazia())
            {
                throw new InvalidOperationException("A pilha de origem está vazia.");
            }
            Carta cartaMovida = pilhas[pilhaOrigem - 1].RemoverCarta();
            if (pilhas[pilhaDestino].EstaVazia())
            {
                if(cartaMovida.GetValor() == Valor.Rei)
                {
                    pilhas[pilhaDestino - 1].AdicionarCarta(cartaMovida);
                    return true;
                }
                else
                {
                    int indiceTopo = pilhas[pilhaOrigem - 1].ContarCartas() - 1;
                    if (indiceTopo >= 0 && !pilhas[pilhaOrigem - 1].ObterCartas()[indiceTopo].Virada)
                    {
                        pilhas[pilhaOrigem - 1].ObterCartas()[indiceTopo].Virar();
                    }
                    pilhas[pilhaOrigem - 1].AdicionarCarta(cartaMovida); 
                    return false;
                }
            }
            if(cartaMovida.GetValor() == pilhas[pilhaDestino - 1].MostrarTopo().GetValor() - 1 && VerificarCor(cartaMovida.GetNaipe(), pilhas[pilhaDestino - 1].MostrarTopo().GetNaipe()))
            {
                pilhas[pilhaDestino - 1].AdicionarCarta(cartaMovida);
                return true;
            }
            else
            {
                int indiceTopo = pilhas[pilhaOrigem - 1].ContarCartas() - 1;
                if (indiceTopo >= 0 && !pilhas[pilhaOrigem - 1].ObterCartas()[indiceTopo].Virada)
                {
                    pilhas[pilhaOrigem - 1].ObterCartas()[indiceTopo].Virar();
                }
                pilhas[pilhaOrigem - 1].AdicionarCarta(cartaMovida);
                pilhas[pilhaOrigem - 1].AdicionarCarta(cartaMovida); 
                return false;
            }
        }
        public bool MoverCartaFundacao(int pilhaOrigem, int fundacaoDestino)
        {
            if(pilhaOrigem < 0 || pilhaOrigem > pilhas.Count || fundacaoDestino < 0 || fundacaoDestino > fundacoes.Count)
            {
                throw new ArgumentOutOfRangeException("Índices de pilha inválidos.");
            }
            if(pilhas[pilhaOrigem - 1].EstaVazia() == true)
            {
                throw new InvalidOperationException("A pilha de origem está vazia.");
            }
            Carta cartaMovida = pilhas[pilhaOrigem - 1].RemoverCarta();
            if(fundacoes[fundacaoDestino - 1].EstaVazia() == true)
            {
                if(cartaMovida.GetValor() == Valor.As)
                {
                    fundacoes[fundacaoDestino - 1].AdicionarCarta(cartaMovida);
                        return true;
                }
                else
                {
                     int indiceTopo = pilhas[pilhaOrigem - 1].ContarCartas() - 1;
                    if (indiceTopo >= 0 && !pilhas[pilhaOrigem - 1].ObterCartas()[indiceTopo].Virada)
                    {
                        pilhas[pilhaOrigem - 1].ObterCartas()[indiceTopo].Virar();
                    }
                    pilhas[pilhaOrigem - 1].AdicionarCarta(cartaMovida);
                    return false;
                }
            }
            else
            {
                if(cartaMovida.GetValor() == fundacoes[fundacaoDestino - 1].MostrarTopo().GetValor() - 1 && cartaMovida.GetNaipe() == fundacoes[fundacaoDestino - 1].MostrarTopo().GetNaipe())
                {
                    fundacoes[fundacaoDestino - 1].AdicionarCarta(cartaMovida);
                        return true;
                }
                else
                {
                     int indiceTopo = pilhas[pilhaOrigem - 1].ContarCartas() - 1;
                    if (indiceTopo >= 0 && !pilhas[pilhaOrigem - 1].ObterCartas()[indiceTopo].Virada)
                    {
                        pilhas[pilhaOrigem - 1].ObterCartas()[indiceTopo].Virar();
                    }
                    pilhas[pilhaOrigem - 1].AdicionarCarta(cartaMovida);
                    return false;
                }
            }
        }
        public void PegarCartaBaralho()
        {
            Carta cartaRetirada = baralho.PegarUltimaCarta();
            cartaRetirada.Virar();
            monte.AdicionarCarta(cartaRetirada);
        }
        public void VoltarBaralho()
        {
            if(baralho.ObterCartas().Count == 0)
            {
                for(int i = monte.ObterCartas().Count - 1; i >= 0; i--)
                {
                    Carta ultimaCarta = monte.ObterCartas()[i];
                    ultimaCarta.Virar();
                    baralho.AdicionarCarta(ultimaCarta);
                }
            }
            else
            {
                throw new InvalidOperationException("O baralho não está vazio");
            }
        }
        public bool PegarCartaMonte(int pilhaDestino)
        {

            if (pilhaDestino < 0 || pilhaDestino > pilhas.Count)
            {
                throw new ArgumentOutOfRangeException("Índice de pilha inválido.");
            }
            if (monte.EstaVazia())
            {
                throw new InvalidOperationException("O monte está vazio.");
            }
            Carta cartaMovida = monte.RemoverCarta();
            if (pilhas[pilhaDestino - 1].EstaVazia())
            {
                if (cartaMovida.GetValor() == Valor.Rei)
                {
                    pilhas[pilhaDestino - 1].AdicionarCarta(cartaMovida);
                    return true;
                }
                else
                {
                    monte.AdicionarCarta(cartaMovida);
                    return false;
                }
            }
            if (cartaMovida.GetValor() == pilhas[pilhaDestino - 1].MostrarTopo().GetValor() - 1 && VerificarCor(cartaMovida.GetNaipe(), pilhas[pilhaDestino - 1].MostrarTopo().GetNaipe()))
            {
                pilhas[pilhaDestino - 1].AdicionarCarta(cartaMovida);
                return true;
            }
            else
            {
                monte.AdicionarCarta(cartaMovida);
                return false;
            }
        }
        public bool PegarCartaMonteFundacao(int fundacaoDestino)
        {
            if (fundacaoDestino < 0 || fundacaoDestino > fundacoes.Count)
            {
                throw new ArgumentOutOfRangeException("Índice de fundação inválido.");
            }
            if (monte.EstaVazia())
            {
                throw new InvalidOperationException("O monte está vazio.");
            }
            Carta cartaMovida = monte.RemoverCarta();
            if (fundacoes[fundacaoDestino - 1].EstaVazia())
            {
                if (cartaMovida.GetValor() == Valor.As)
                {
                    fundacoes[fundacaoDestino - 1].AdicionarCarta(cartaMovida);
                    return true;
                }
                else
                {
                    monte.AdicionarCarta(cartaMovida);
                    return false;
                }
            }
            if (cartaMovida.GetValor() == fundacoes[fundacaoDestino - 1].MostrarTopo().GetValor() - 1 && cartaMovida.GetNaipe() == fundacoes[fundacaoDestino - 1].MostrarTopo().GetNaipe())
            {
                fundacoes[fundacaoDestino - 1].AdicionarCarta(cartaMovida);
                return true;
            }
            else
            {
                monte.AdicionarCarta(cartaMovida);
                return false;
            }
        }
        public bool VerficarVitoria()
        {
            List<bool> fundacoesCheias = new List<bool>();
            foreach (Pilha fundacao in fundacoes)
            {
                if (fundacao.ContarCartas() < 13)
                {
                    fundacoesCheias.Add(false);
                }
                else 
                {
                    fundacoesCheias.Add(true);
                }
            }
            if (fundacoesCheias.Contains(false))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
