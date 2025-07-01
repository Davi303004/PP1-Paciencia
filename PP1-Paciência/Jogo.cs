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
        private List<Pilha> pilhas;
        private List<Pilha> fundacoes;

        public void IniciarJogo()
        {
            baralho = new Baralho();
            baralho.Embaralhar();
            pilhas = new List<Pilha>();
            fundacoes = new List<Pilha>();

            for (int i = 1; i <= 7; i++)
            {
                List<Carta> cartasDaPilha = baralho.SepararCartas(i);
                Pilha novaPilha = new Pilha();
                novaPilha.AdicionarCarta(cartasDaPilha);
                pilhas.Add(novaPilha);
            }

            for (int i = 0; i < 4; i++)
            {
                fundacoes.Add(new Pilha());
            }
        }
        private bool VerificarCor(Naipe n1, Naipe n2)
        {
            return (n1 == Naipe.Copas || n1 == Naipe.Ouros) && (n2 == Naipe.Paus || n2 == Naipe.Espadas) ||
                   (n1 == Naipe.Paus || n1 == Naipe.Espadas) && (n2 == Naipe.Copas || n2 == Naipe.Ouros);
        }
        public bool MoverCarta(int pilhaOrigem, int pilhaDestino)
        {
            if(pilhaOrigem < 0 || pilhaOrigem >= pilhas.Count || pilhaDestino < 0 || pilhaDestino >= pilhas.Count)
            {
                throw new ArgumentOutOfRangeException("Índices de pilha inválidos.");
            }
            if (pilhas[pilhaOrigem - 1].EstaVazia() == true)
            {
                throw new InvalidOperationException("A pilha de origem está vazia.");
            }
            Carta cartaMovida = pilhas[pilhaOrigem - 1].RemoverCarta();
            if (pilhas[pilhaDestino].EstaVazia() ||
                (cartaMovida.GetValor() == pilhas[pilhaDestino - 1].MostrarTopo().GetValor() - 1 &&
                 VerificarCor(cartaMovida.GetNaipe(), pilhas[pilhaDestino - 1].MostrarTopo().GetNaipe()) == true))
            {
                pilhas[pilhaDestino - 1].AdicionarCarta(cartaMovida);
                return true;
            }
            else
            {
                pilhas[pilhaOrigem - 1].AdicionarCarta(cartaMovida); 
                return false;
            }
        }
        public bool VerficarVitoria()
        {
            foreach (Pilha fundacao in fundacoes)
            {
                if (fundacao.ContarCartas() < 13)
                {
                    return false;
                }
                else 
                {

                }
            }
        }
    }
}
