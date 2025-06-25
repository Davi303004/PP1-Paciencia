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

        public void Embaralhar()
        {
            Random random = new Random();
            cartas = cartas.OrderBy(c => random.Next()).ToList();
        }
    }
}
