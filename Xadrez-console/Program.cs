using System;
using tabuleiro;

namespace Xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao P;
            P = new Posicao(3, 4); // criado novo objeto

            Console.WriteLine("Posição: " + P);

            Tabuleiro tab = new Tabuleiro(8, 8);


        }

        
    }
}
