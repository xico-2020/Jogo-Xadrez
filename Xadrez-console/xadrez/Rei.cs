//using System;
//using System.Collections.Generic;
//using System.Text;
using tabuleiro;

namespace xadrez
{
    class Rei : Peca      // Herda da classe peça
    {
        private PartidaDeXadrez partida; // destina-se a que o Rei tenha acesso à partida para a jogada especial
        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)  // Construtor que recebe o Tabuleiro tab e a Cor cor e vai passar esses dados para a classe Peca.
        {     // Foi criado um objeto Rei, passando o tabuleiro e cor para a superClassse Peca. O construtor do Rei recebe a partida como argumento.
            this.partida = partida;  //para o acesso à partida
        }

        public override string ToString()
        {
            return "R";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;  // retorna se é nula (não tem peça) ou se a peça é diferente da cor da peça em jogo.
                                               // Na prática a peça pode ser movida se a casa destino estiver sem peça ou tiver uma peça adversária.
        }

        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p = tab.peca(pos);  // selecionar a peça na posição atual  
            return p != null && p is Torre && p.cor == cor && qteMovimentos == 0;
        }
        public override bool[,] movimentosPossiveis()  // override porque está a sobreescrever o método da Classe Peca.
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];
            Posicao pos = new Posicao(0, 0);

            // acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // direita
            pos.definirValores(posicao.linha , posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // sudeste
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // baixo
            pos.definirValores(posicao.linha - 1, posicao.coluna );
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // sudoeste
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // esquerda
            pos.definirValores(posicao.linha , posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // noroeste
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // #jogada especial roque
            
            if (qteMovimentos == 0 && !partida.xeque)
            {
                // #jogada especial roque pequeno
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3); // posição esperada em que a Torre esteja para poder fazer Roque.
                if (testeTorreParaRoque(posT1))  // ser for possivel (verdadeiro)
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);  // se as duas posiçóes ao lado do Rei estiverem vazias
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if (tab.peca(p1) == null && tab.peca(p2) == null)
                    {
                        mat[posicao.linha, posicao.coluna + 2] = true; // a matriz dos movimentos possiveis para a posição do rei linha e coluna +2 recebe True.
                    }
                }

                // #jogada especial roque grande
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4); // posição esperada em que a Torre esteja para poder fazer Roque.
                if (testeTorreParaRoque(posT2))  // ser for possivel (verdadeiro)
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);  // se as duas posiçóes ao lado do Rei estiverem vazias
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null)
                    {
                        mat[posicao.linha, posicao.coluna - 2] = true; // a matriz dos movimentos possiveis para a posição do rei linha e coluna -2 recebe True.
                    }
                }
            }


            return mat;
        }
    } 
}
