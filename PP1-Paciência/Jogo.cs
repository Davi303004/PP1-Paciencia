using System;
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
        public bool MoverCartaPilha(int pilhaOrigem, int pilhaDestino, int quantidadeCartas)
        {
            if (pilhaOrigem < 1 || pilhaOrigem > pilhas.Count || pilhaDestino < 1 || pilhaDestino > pilhas.Count || pilhaOrigem == pilhaDestino)
            {
                throw new ArgumentOutOfRangeException("Índices de pilha inválidos.");
            }

            var origem = pilhas[pilhaOrigem - 1];
            var destino = pilhas[pilhaDestino - 1];

            if (origem.ContarCartas() < quantidadeCartas)
            {
                throw new InvalidOperationException("A pilha de origem não possui cartas suficientes.");
            }

            int indiceInicial = origem.ContarCartas() - quantidadeCartas;
            var cartasMovidas = origem.RemoverCarta(quantidadeCartas);

            var cartaTopoMovida = cartasMovidas[0];
            if (destino.EstaVazia())
            {
                if (cartaTopoMovida.GetValor() != Valor.Rei) 
                { 
                    return false; 
                }
            }
            else
            {
                var cartaTopoDestino = destino.MostrarTopo();
                if (cartaTopoMovida.GetValor() != cartaTopoDestino.GetValor() - 1 ||
                    !VerificarCor(cartaTopoMovida.GetNaipe(), cartaTopoDestino.GetNaipe()))
                {
                    return false;
                }
            }
            destino.AdicionarCarta(cartasMovidas);

            if (!origem.EstaVazia())
            {
                var novaTopo = origem.MostrarTopo();
                if (!novaTopo.Virada) novaTopo.Virar();
            }

            return true;
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
            Carta cartaMovida = pilhas[pilhaOrigem - 1].MostrarTopo();
            if (pilhas[pilhaDestino - 1].EstaVazia())
            {
                if(cartaMovida.GetValor() == Valor.Rei)
                {
                    pilhas[pilhaOrigem - 1].RemoverCarta();
                    if (!pilhas[pilhaOrigem - 1].EstaVazia())
                    {
                        if (!pilhas[pilhaOrigem - 1].MostrarTopo().Virada)
                        {
                            pilhas[pilhaOrigem - 1].MostrarTopo().Virar();
                        }
                    }
                    pilhas[pilhaDestino - 1].AdicionarCarta(cartaMovida);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if(cartaMovida.GetValor() == pilhas[pilhaDestino - 1].MostrarTopo().GetValor() - 1 && VerificarCor(cartaMovida.GetNaipe(), pilhas[pilhaDestino - 1].MostrarTopo().GetNaipe()))
            {
                pilhas[pilhaOrigem - 1].RemoverCarta();
                if (!pilhas[pilhaOrigem - 1].EstaVazia())
                {
                    if (!pilhas[pilhaOrigem - 1].MostrarTopo().Virada)
                    {
                        pilhas[pilhaOrigem - 1].MostrarTopo().Virar();
                    }
                }
                pilhas[pilhaDestino - 1].AdicionarCarta(cartaMovida);
                return true;
            }
            else
            { 
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
            Carta cartaMovida = pilhas[pilhaOrigem - 1].MostrarTopo();
            if(fundacoes[fundacaoDestino - 1].EstaVazia() == true)
            {
                if(cartaMovida.GetValor() == Valor.As)
                {
                    pilhas[pilhaOrigem - 1].RemoverCarta();
                    if (!pilhas[pilhaOrigem - 1].EstaVazia())
                    {
                        if (!pilhas[pilhaOrigem - 1].MostrarTopo().Virada)
                        {
                            pilhas[pilhaOrigem - 1].MostrarTopo().Virar();
                        }
                    }
                    fundacoes[fundacaoDestino - 1].AdicionarCarta(cartaMovida);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if(cartaMovida.GetValor() == fundacoes[fundacaoDestino - 1].MostrarTopo().GetValor() + 1 && cartaMovida.GetNaipe() == fundacoes[fundacaoDestino - 1].MostrarTopo().GetNaipe())
                {
                    pilhas[pilhaOrigem - 1].RemoverCarta();
                    if (!pilhas[pilhaOrigem - 1].EstaVazia())
                    {
                        if (!pilhas[pilhaOrigem - 1].MostrarTopo().Virada)
                        {
                            pilhas[pilhaOrigem - 1].MostrarTopo().Virar();
                        }
                    }
                    fundacoes[fundacaoDestino - 1].AdicionarCarta(cartaMovida);
                    return true;
                }
                else
                {
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
                    monte.RemoverCarta();
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
            if (cartaMovida.GetValor() == fundacoes[fundacaoDestino - 1].MostrarTopo().GetValor() + 1 && cartaMovida.GetNaipe() == fundacoes[fundacaoDestino - 1].MostrarTopo().GetNaipe())
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
