using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BibliotecaErro;

namespace CadastroPessoasSQL
{
    public partial class Form2 : Form
    {
        Pessoa pessoa = new Pessoa();
        public Form2()
        {
            InitializeComponent();
        }
        
        private void mostra()
        {
            textBox1.Text = pessoa.getID();
            textBox2.Text = pessoa.getNome();
            textBox3.Text = pessoa.getIdade();
            if (pessoa.getSexo() == "Feminino")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            PessoaBLL.Conectar();
            PessoaBLL.populaDR();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            PessoaBLL.Desconectar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pessoa = PessoaBLL.getPrimeiro();
            mostra();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pessoa = PessoaBLL.getAnterior();
            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());
            else
                mostra();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pessoa = PessoaBLL.getProximo();
            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());
            else
                mostra();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pessoa = PessoaBLL.getUltimo();
            mostra();
        }
    }
}
