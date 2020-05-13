using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;
using xadrez;

namespace Xadrez_console
{
    class Tela
    {

        public static void imprimirPartida(PartidaDeXadrez partida)
        {
            imprimirTabuleiro(partida.tab);
            
            imprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.turno);
            Console.WriteLine("Aguarda jogada : " + partida.jogadorAtual);
        }

        public static void imprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach(Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
        public static void imprimirTabuleiro(Tabuleiro tab) // Chamada de método que vai mostrar o tabuleiro.
        {
            Console.WriteLine();
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(" " + (8 - i) + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    imprimirPeca(tab.peca(i, j));
                    /*imprimirPeca(tab.peca(i, j) + " "); // (tab.peca) - Chama o metodo peca no objeto tab em Tabuleiro(ver acima).
                          // Este método foi criado pois como Peca é private, não permite acesso a ela fora da classe.*/
                }
                Console.WriteLine();
            }
            Console.WriteLine("   a b c d e f g h");
        }
        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis) // Sobrecarga do método para passar a matriz bool com as posições possiveis.
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor; // Guarda a cor de fundo original. 
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
            Console.WriteLine();
            for (int i = 0; i < tab.linhas; i++)
            {   
                Console.Write(" " + (8 - i) + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (posicoesPossiveis[i, j]) // Tambem possivel ( if (posicoesPossiveis[i,j] == true) : está a validar se forem posicoes possiveis mostra o fundo de outra cor.
                    {
                        Console.BackgroundColor = fundoAlterado;
                    } else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                        imprimirPeca(tab.peca(i, j));
                              /*imprimirPeca(tab.peca(i, j) + " "); // (tab.peca) - Chama o metodo peca no objeto tab em Tabuleiro(ver acima).
                               Este método foi criado pois como Peca é private, não permite acesso a ela fora da classe.*/
                        Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("   a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + ""); // concatenado com "" só para forçar a ser string vazio.
            return new PosicaoXadrez(coluna, linha);
        }

        public static void imprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor; // Guarda a cor atual na variavel aux
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;  // repoe a cor inicial
                }
                Console.Write(" ");
            }


           
        }
    }
}
