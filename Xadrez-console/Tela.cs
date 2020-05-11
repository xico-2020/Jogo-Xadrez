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
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (tab.peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tab.peca(i, j) + " "); // (tab.peca) - Chama o metodo peca no objeto tab em Tabuleiro.
                              // Este método foi criado pois como Peca é private, não permite acesso a ela fora da classe.
                    }
                    
                }
                Console.WriteLine();
            }

        }
    }
}
