using System;
using System.Collections.Generic;
using System.Text;
using xadrez;

namespace tabuleiro
{
    abstract class Peca  // Classe abstrata pois tem pelo menos um método abstrato (movimentosPossiveis, neste caso).
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; } // Pode ser acedida por qualquer classe, mas só modificada por si e pela sua subclasse.
        public  int qteMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca( Tabuleiro tab, Cor cor )  // Argumento Posição náo posto pois é nulo ( ver linha abaixo)
        {
            this.posicao = null;  // quando crio uma peça, ela ainda não tem posição, logo é nula.  
            this.tab = tab;
            this.cor = cor;
            this.qteMovimentos = 0; // inicializada a zero e por isso não foi passada como argumento acima.

        }

        public void incrementarQteMovimentos()
        {
            qteMovimentos++;
        }

        public abstract bool[,] movimentosPossiveis();   // Método abstrato pois não tem implementação nesta Classe. Matriz de valores pois vai informar se a posição da peça a mover pode ser ou não. Retorna Verdadeiro ou Falso.
       
    }
}
