using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PP1_Paciência.Cartas;

namespace PP1_Paciência
{
    class Baralho
    {
        private List<Carta> cartas;
        public Baralho()
        {
            cartas = new List<Carta>();
            CriarBaralho();
        }
        private void CriarBaralho()
        {
            foreach (Naipe naipe in Enum.GetValues(typeof(Naipe)))
            {
                foreach (Valor valor in Enum.GetValues(typeof(Valor)))
                {
                    cartas.Add(new Carta((Naipe)naipe,(Valor)valor));
                }
            }
        }
        public List<Carta> ObterCartas()
        {
            return cartas;
        }
        public List<Carta> SepararCartas(int quantidade)
        {
            if (quantidade < 1 || quantidade > cartas.Count)
            {
                throw new ArgumentOutOfRangeException("Quantidade deve ser entre 1 e o número de cartas restantes no baralho.");
            }
            List<Carta> cartasRetiradas = cartas.Take(quantidade).ToList();
            cartas.RemoveRange(0, quantidade);
            return cartasRetiradas;
        }
        public void Embaralhar()
        {
            Random random = new Random();
            cartas = cartas.OrderBy(c => random.Next()).ToList();
        }
        public void RemoverCarta(Carta carta)
        {
            if (cartas.Contains(carta))
            {
                cartas.Remove(carta);
            }
            else
            {
                throw new InvalidOperationException("Uma ou mais cartas não estão no baralho.");
            }
        }
    }
}
