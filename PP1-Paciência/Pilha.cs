using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PP1_Paciência.Cartas;

namespace PP1_Paciência
{
    class Pilha
    {
        private List<Carta> cartas;
        public Pilha()
        {
            cartas = new List<Carta>();
        }
        public Pilha(List<Carta> cartas)
        {
            this.cartas = cartas;
        }
        public void AdicionarCarta(Carta carta)
        {
            cartas.Add(carta);
        }
        public void AdicionarCarta(List<Carta> cartasAdicionadas)
        {
            foreach (Carta carta in cartasAdicionadas)
            {
                cartas.Add(carta);
            }
            
        }
        public Carta RemoverCarta()
        {
            if (cartas.Count == 0)
                throw new InvalidOperationException("A pilha está vazia.");
            Carta carta = cartas[cartas.Count - 1];
            cartas.RemoveAt(cartas.Count - 1);
            return carta;
        }
        public bool EstaVazia()
        {
            return cartas.Count == 0;
        }
        public int ContarCartas()
        {
            return cartas.Count;
        }
        public List<Carta> ObterCartas()
        {
            return cartas;
        }
        public Carta MostrarTopo()
        {
            if (cartas.Count == 0)
                throw new InvalidOperationException("A pilha está vazia.");
            return cartas[cartas.Count - 1];
        }
    }
}
