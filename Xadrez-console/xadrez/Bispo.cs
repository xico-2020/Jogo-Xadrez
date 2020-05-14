using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
    class Bispo : Peca      // Herda da classe peça
    {
        public Bispo(Tabuleiro tab, Cor cor) : base(tab, cor)  // Construtor que recebe o Tabuleiro tab e a Cor cor e vai passar esses dados para a classe Peca.
        {     // Foi criado um objeto Rei, passando o tabuleiro e cor para a superClassse Peca.

        }

        public override string ToString()
        {
            return "B";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;  // retorna se é nula (não tem peça) ou se a peça é diferente da cor da peça em jogo.
                                               // Na prática a peça pode ser movida se a casa destino estiver sem peça ou tiver uma peça adversária.
        }
        public override bool[,] movimentosPossiveis()  // override porque está a sobreescrever o método da Classe Peca.
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];
            Posicao pos = new Posicao(0, 0);

            // noroeste
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            }

            // nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            }


            // sudeste
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            }


            // sudoeste
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            }
            
            return mat;
        }
    }
}
