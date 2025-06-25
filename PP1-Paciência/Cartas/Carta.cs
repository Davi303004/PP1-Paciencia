using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP1_Paciência.Cartas
{
    class Carta
    {
        private Naipe Naipe;
        private Valor Valor;
        public bool Virada;
        public Carta(Naipe naipe, Valor valor)
        {
            Naipe = naipe;
            Valor = valor;
            Virada = false;
        }
        public Naipe GetNaipe()
        {
            return Naipe;
        }
        public Valor GetValor()
        {
            return Valor;
        }
        public void Virar()
        {
            Virada = !Virada;
        }


    }
}
