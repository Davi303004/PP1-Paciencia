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
            baralho.ObterCartas().ForEach(carta =>
            {
                Console.WriteLine($"Carta: {carta.GetValor()} de {carta.GetNaipe()} - Virada: {carta.Virada}");
            });
        }
    }
}
