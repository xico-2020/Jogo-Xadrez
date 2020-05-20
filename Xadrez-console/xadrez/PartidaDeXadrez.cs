//using System;
using System.Collections.Generic; // para poder mexer com conjumtos
//using System.Reflection.Metadata.Ecma335;
//using System.Text;
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
        public bool xeque { get; private set; }


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false; // indica que a partida ainda não está terminada.  
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino) // Executa um movimento da posição X para Y.
        {
            Peca p = tab.retirarPeca(origem);  // Retira a peça de onde estava.
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino); // Caso exista, retira a peça que estava no destino.
            tab.colocarPeca(p, destino); // Coloca peça de origem no destino.
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            // #jogada especial roque pequeno;

            if (p is Rei && destino.coluna == origem.coluna + 2)  // se for Rei e movi o Rei duas casas para a direita
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3); // a origem da Torre é tres colunas à direita do Rei 
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1); // a posiçáo destino da Torre vai ser linha do Rei e coluna do Rei + 1
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            // #jogada especial roque grande;

            if (p is Rei && destino.coluna == origem.coluna - 2)  // se for Rei e movi o Rei duas casas para a direita
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4); // a origem da Torre é tres colunas à direita do Rei 
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1); // a posiçáo destino da Torre vai ser linha do Rei e coluna do Rei + 1
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            return pecaCapturada;
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);

            if (estaEmCheque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Não pode colocar-se em cheque!");
            }
            if (estaEmCheque(adversaria(jogadorAtual)))  // se o cheque for para a equipa adversária pode ser, se for por em xeque a própria equipa, não pode.
            {
                xeque = true;
            } else
            {
                xeque = false;
            }
            if (testeXequemate(adversaria(jogadorAtual)))
            {
                terminada = true;
            } else
            {
                turno++;
                mudaJogador();
            }
            
        }

        public void desfazMovimento( Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);  // Retira a peça na posição para onde tinha ido.
            p.decrementarQteMovimentos();
            if (pecaCapturada!= null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);

            // #jogada especial roque pequeno;

            if (p is Rei && destino.coluna == origem.coluna + 2)  // se for Rei e movi o Rei duas casas para a direita
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3); // a origem da Torre é tres colunas à direita do Rei 
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1); // a posiçáo destino da Torre vai ser linha do Rei e coluna do Rei + 1
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }

            // #jogada especial roque grande;

            if (p is Rei && destino.coluna == origem.coluna - 2)  // se for Rei e movi o Rei duas casas para a direita
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4); // a origem da Torre é quatro colunas à esquerda do Rei e coluna do Rei - 1
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }
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
          if (!tab.peca(origem).movimentoPossivel(destino))
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

        private Cor adversaria(Cor cor)  //Método apenas usado nesta Classe. Qual é a cor adversária de uma dada cor?
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            } else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)  // devolve um Rei de uma determinada cor.
        {
            foreach(Peca x in pecasEmJogo(cor))  // para cada peça x no conjunto de peças em jogo ...
            {
                if (x is Rei)  // Se x é uma instancia da classe Rei ... (Peca é uma super Classe e Rei é uma sub Classe de Peca)
                               // Para testar se uma variavel do tipo da super Classe é uma instancia de alguma sub Classe tenho que usar a palavra is.
                {
                    return x;  // é o Rei da cor.
                }
            }
            return null;  // não encontrou nada.
        }


        public bool estaEmCheque(Cor cor) // analisa todos os movimentos possiveis das peças adversárias.
        {
            Peca R = rei(cor); // Peca R(variavel aqui criada) rebebe o rei da cor.
            if (R == null)
            {
                throw new TabuleiroException("Não existe Rei da cor " + cor + "no tabuleiro!");
            }

            foreach (Peca x in pecasEmJogo(adversaria(cor)))  // ver pecas em jogo na cor adversaria.
            {
                bool[,] mat = x.movimentosPossiveis();  // matriz de movimentos possiveis.
                if (mat[R.posicao.linha, R.posicao.coluna]) // Se na matriz de movimentos possiveis da peça adversária x na posição do Rei for verdadeiro, significa que pode fazer cheque ao Rei
                {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequemate(Cor cor)
        {
            if (!estaEmCheque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis(); // Pra percorrer todas as peças x naas pecas em jogo essa cor, para encontrar uma que tire do cheque.
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])   // se verdadeiro ...
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);  // tentar executar um movimento da peça x para a nova posição (i, J).
                            bool testeXeque = estaEmCheque(cor);  // Testa se esta em cheque o rei da cor jogada.
                            desfazMovimento(origem, destino, pecaCapturada);  // Desfaz o movimento.
                            if (!testeXeque) // Se o moviment feito não colocar o Rei em cheque, então existe movimento para tirar do cheque. Logo não está em xeque mate.
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;  // caso não exista jogada, retorna verdadeiro. (Xeque mate)
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao()); // coloca uma peça numa nova posição do tabuleiro
            pecas.Add(peca);  //  Adiciono a peça ao conjunto pecas.
        }
        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this)); // auto referencia para a classe Rei (conf construtor Rei c/ ref partida)
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));

            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca));


            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));

            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta));
            
        
        }
       
    }
}
