using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace tabuleiro
{
    class Posicao
    {
        public int linha { get; set; }  // Pode ser acedido e alterado em qualquer classe.
        public int coluna { get; set; }

        public Posicao(int linha, int coluna) // construtor que atribui diretamente os valores para o novo objeto quando for instanciado numa linha de comando.
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        public void definirValores(int linha, int coluna)  // Define valores da posição
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        public override string ToString()
        {
            return linha
                + ", "
                + coluna;

        }

    }
}
