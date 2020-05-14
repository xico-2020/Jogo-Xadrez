using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
    class Peao : Peca      // Herda da classe peça
    {
        public Peao(Tabuleiro tab, Cor cor) : base(tab, cor)  // Construtor que recebe o Tabuleiro tab e a Cor cor e vai passar esses dados para a classe Peca.
        {     // Foi criado um objeto Rei, passando o tabuleiro e cor para a superClassse Peca.

        }

        public override string ToString()
        {
            return "P";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;  // retorna se é nula (não tem peça) ou se a peça é diferente da cor da peça em jogo.
                                               // Na prática a peça pode ser movida se a casa destino estiver sem peça ou tiver uma peça adversária.
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p.cor != cor;
        }

        private bool livre(Posicao pos)
        {
            return tab.peca(pos) == null;
        }
        public override bool[,] movimentosPossiveis()  // override porque está a sobreescrever o método da Classe Peca.
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];
            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branca)
            {
                pos.definirValores(posicao.linha - 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha - 2, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                else
                {
                    pos.definirValores(posicao.linha + 1, posicao.coluna);
                    if (tab.posicaoValida(pos) && livre(pos))
                    {
                        mat[pos.linha, pos.coluna] = true;
                    }

                    pos.definirValores(posicao.linha + 2, posicao.coluna);
                    if (tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0)
                    {
                        mat[pos.linha, pos.coluna] = true;
                    }

                    pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
                    if (tab.posicaoValida(pos) && existeInimigo(pos))
                    {
                        mat[pos.linha, pos.coluna] = true;
                    }
                    pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
                    if (tab.posicaoValida(pos) && existeInimigo(pos))
                    {
                        mat[pos.linha, pos.coluna] = true;
                    }

                }

            }

            return mat;
        }
    }
}
