﻿using System;
using tabuleiro;
using xadrez;

namespace Xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.terminada)
                {
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();  // Método que vai ler do teclado uma posição do xadrez e guarda numa posição de matriz.

                    bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis(); // A partir mda posição de origem digitada, vai aceder à peça de origem no tabuleiro e ver quais os movimentos possiveis dela e guarda na matriz posicoesPossiveis.

                    Console.Clear();  // Estas duas linhas destinam-se a mostrar o tabuleiro com as posições possiveis de jogo noutra cor.
                    Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                  
                    partida.ExecutaMovimento(origem, destino);
                }

                
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            

        }


    }
}
