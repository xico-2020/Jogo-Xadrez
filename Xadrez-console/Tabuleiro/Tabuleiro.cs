using System;
using System.Collections.Generic;
using System.Text;



namespace tabuleiro
{
    class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas; // Matriz de peças. É privativa pois não pode ser acedida de fora.


        public Tabuleiro( int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas]; // Matriz de pecas que recebe o numero de linha e coluna.
        }

        public Peca peca(int linha, int coluna) // Como Peca é private, para dar acesso a ela noutra classe tenho de criar este método.
            // Recebe como argumento linha, coluna e retorna a matriz pecas na posição linha coluna.
        {
            return pecas[linha, coluna]; 
        }

        public void colocarPeca(Peca p, Posicao pos)   // Método para colocar peca no tabuleiro
        {
            pecas[pos.linha, pos.coluna] = p; // Acede a matriz na linha pos.linha, pos.coluna e recebe a peca p .
            p.posicao = pos;  // Informa a posicao da peça p na posição pos.
        } 

    }
}
