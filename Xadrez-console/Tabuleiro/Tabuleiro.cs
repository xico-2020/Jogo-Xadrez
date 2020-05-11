using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public Peca peca(Posicao pos) // Sobrecarga do método peca que só recebe pos da classe Posicao.
        {
            return pecas[pos.linha, pos.coluna];
        }

        public bool existePeca(Posicao pos) // Método que passa a posição pos e chama o método validarPosicao. Se não for válida lança exceção.
        {
            validarPosicao(pos);
            return peca(pos) != null;
        }
        public void colocarPeca(Peca p, Posicao pos)   // Método para colocar peca no tabuleiro
        {
            if (existePeca(pos))  // Faz dois testes, chama método existePeca que por sua vez chama o validarPosicao.
            {
                throw new TabuleiroException("Já existe uma peça nessa posição"); 
            }
            pecas[pos.linha, pos.coluna] = p; // Acede a matriz na linha pos.linha, pos.coluna e recebe a peca p .
            p.posicao = pos;  // Informa a posicao da peça p na posição pos.
        } 

        public bool posicaoValida(Posicao pos)  // Valida se o numero de linha e coluna é válido.
        {
            if (pos.linha<0 || pos.linha >= linhas || pos.coluna < 0 || pos.coluna >= colunas)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void validarPosicao(Posicao pos)  // método que por cada posicao não válida lança uma exceção personalizada
        {
            if (!posicaoValida(pos))   // condição se não for posíção válida (! indica negação)
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }

    }
}
