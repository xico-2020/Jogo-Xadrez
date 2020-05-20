//using System;
//using System.Collections.Generic;
//using System.Text;
using tabuleiro;

namespace xadrez
{
    class PosicaoXadrez  // define as posições no tabuleiro
    {
        public char coluna { get; set; }
        public int linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        public Posicao toPosicao() // Converte as linhas 1- 8 e colunas a - h nas posições 0 - 7 da tabela.
        {
            return new Posicao(8 - linha, coluna - 'a'); // converte o numero da linha na posicao da tabela 
                         // (ex. posicao 3 tabuleiro é 8-3= 5 na tabela (sendo que linha 8 = pos 0 e linha 1 = pos 7).
                         // (ex. a-a = 0, a-b= 1 na tabela (sendo que coluna a = pos 0 e coluna h = pos 7).
        }

        public override string ToString()
        {
            return "" + coluna + linha;
        }
    }
}
