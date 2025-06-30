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
            Baralho baralho = new Baralho();
            baralho.Embaralhar();
            List<Pilha> pilhas = new List<Pilha>();
            for (int i = 1; i <= 7; i++)
            {
                List<Carta> cartasDaPilha = baralho.SepararCartas(i);
                Pilha novaPilha = new Pilha();
                novaPilha.AdicionarCarta(cartasDaPilha);
                pilhas.Add(novaPilha);
                Console.WriteLine($"Pilha {i}: {novaPilha.ContarCartas()} Cartas");

            }
            int j = 1;
            foreach (Pilha pilha in pilhas)
            {
                int k = 1;
                foreach (Carta carta in pilha.ObterCartas())
                {
                    Console.WriteLine($"Pilha {j} : {k}° Carta: {carta.GetValor()} de {carta.GetNaipe()}");
                    k++;
                }
                j++;
            }
        }
    }
}
