﻿using System;
using tabuleiro;
using xadrez;

namespace Xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));  // coloca Torre preta na posição  0,0 .
            tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));  // coloca Torre preta na posição  1,3 .
            tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(2, 4));  // coloca Rei preto na posição  2,4 .

            Tela.imprimirTabuleiro(tab);

        }

        
    }
}
