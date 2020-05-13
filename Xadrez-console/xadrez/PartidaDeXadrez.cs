using System;
using System.Collections.Generic; // para poder mexer com conjumtos
using System.Text;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }  // Não deixa modificar o Tabuleiro fora da classe.
        public int turno { get; private set; } // A cada jogada o turno é incrementado.
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;  // define o conjunto de peças (coleção de dados)
        private HashSet<Peca> capturadas;  // define o conjunto de peças capturadas


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false; // indica que a partida ainda não está terminada.   
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino) // Executa um movimento da posição X para Y.
        {
            Peca p = tab.RetirarPeca(origem);  // Retira a peça de onde estava.
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino); // Caso exista, retira a peça que estava no destino.
            tab.colocarPeca(p, destino); // Coloca peça de origem no destino.
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }


        public void validarPosicaoOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)  // Se no tabuleiro a posição escolhida for nula...
            {
                throw new TabuleiroException("Não existe peça de origem na posição escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).cor)   // valida se a cor do jogador escolhida está certa. Caso errado lança exceção.
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())  // Se não existem movimentos possiveis para a peça, lanço exceção.
            {
                throw new TabuleiroException("Não existem movimentos possiveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDestino(Posicao origem, Posicao destino)
        {
          if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            } else {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)  // verifica quais as peças capturadas da cor passada como argumento.
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            
                foreach (Peca x in capturadas)  // por cada peça x nas peças capturadas ...
                {
                    if (x.cor == cor)
                    {
                        aux.Add(x);  // adiciona a peça no conjunto aux.
                    }
                }
             return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca x in pecas)  // por cada peça x no conjunto pecas ...
            {
                if (x.cor == cor)
                {
                    aux.Add(x);  // adiciona a peça no conjunto aux.
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));  // exclui as peças capturadas.
            return aux;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao()); // coloca uma peça numa nova posição do tabuleiro
            pecas.Add(peca);  //  Adiciono a peça ao conjunto pecas.
        }
        private void colocarPecas()
        {
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
          
            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
            
        }

    }
}
