using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez_console
{
    class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab) // Chmada de método que vai mostrar o tabuleiro.
        {

            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (tab.peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        imprimirPeca(tab.peca(i, j));
                        Console.Write(" ");
                        /*Console.Write(tab.peca(i, j) + " "); // (tab.peca) - Chama o metodo peca no objeto tab em Tabuleiro.
                              // Este método foi criado pois como Peca é private, não permite acesso a ela fora da classe.*/
                    }
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine(" a b c d e f g h");

        }
        public static void imprimirPeca(Peca peca)
        {
            if (peca.cor == Cor.Branca)
            {
                Console.Write(peca);
            } else
            {
                ConsoleColor aux = Console.ForegroundColor; // Guarda a cor atual na variavel aux
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;  // repoe a cor inicial
            } 
        }
    }
}
