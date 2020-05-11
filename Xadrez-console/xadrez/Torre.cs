using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
    class Torre : Peca      // Herda da classe peça
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)  // Construtor que recebe o Tabuleiro tab e a Cor cor e vai passar esses dados para a classe Peca.
        {     // Foi criado um objeto Rei, passando o tabuleiro e cor para a superClassse Peca.

        }

        public override string ToString()
        {
            return "T";
        }
    }
}