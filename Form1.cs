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
    public partial class Form1 : Form
    {
        Random id = new Random();
        Pessoa pessoa = new Pessoa();
        DataTable table = PessoaBLL.getDT();
        int index;
        public Form1()
        {
            InitializeComponent();
        }

        private void getPessoa()
        {
            textBox3.Text = pessoa.getIdade();
            textBox2.Text = pessoa.getNome();
            if(pessoa.getSexo() == "Feminino")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
        }

        private void LimpaTudo()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            if (radioButton1.Checked)
            {
                radioButton1.Checked = false;
            }
            if (radioButton2.Checked)
            {
                radioButton2.Checked = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PessoaBLL.Conectar();
            if(Erro.getErro())
            {
               MessageBox.Show(Erro.getMsg());
               Application.Exit();
            }
            dataGridView1.DataSource = table;
            PessoaBLL.Desconectar();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            PessoaBLL.Desconectar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pessoa.setID(id.Next(10000,30000).ToString());
            pessoa.setNome(textBox2.Text);
            if (radioButton1.Checked)
            {
                pessoa.setSexo("Feminino");
            }
            if (radioButton2.Checked)
            {
                pessoa.setSexo("Masculino");
            }
            pessoa.setIdade(textBox3.Text);
            PessoaBLL.ValidaDados(pessoa, 'i');
            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());
            else
            {
                MessageBox.Show("Dados Inseridos com Sucesso!");
                MessageBox.Show("O ID é: " + pessoa.getID());
                textBox1.Text = pessoa.getID();
                table.Rows.Add(pessoa.getID(), pessoa.getNome(), 
                    pessoa.getIdade(), pessoa.getSexo());
            }        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LimpaTudo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pessoa.setID(textBox1.Text);
            PessoaBLL.ValidaCodigo(pessoa, 'c');
            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());
            else
            {
                getPessoa();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pessoa.setID(textBox1.Text);
            pessoa.setNome(textBox2.Text);
            if (radioButton1.Checked)
            {
                pessoa.setSexo("Feminino");
            }
            if (radioButton2.Checked)
            {
                pessoa.setSexo("Masculino");
            }
            pessoa.setIdade(textBox3.Text);
            PessoaBLL.ValidaDados(pessoa, 'a');
            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());
            else
            {
                MessageBox.Show("Dados Alterados com Sucesso!");
                DataGridViewRow newdata = dataGridView1.Rows[index];
                newdata.Cells[0].Value = pessoa.getID();
                newdata.Cells[1].Value = pessoa.getNome();
                newdata.Cells[2].Value = pessoa.getIdade();
                newdata.Cells[3].Value = pessoa.getSexo();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pessoa.setID(textBox1.Text);
            PessoaBLL.ValidaCodigo(pessoa, 'e');
            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());
            else
            {
                MessageBox.Show("Pessoa Excluida!");
                index = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.Rows.RemoveAt(index);
                LimpaTudo();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String sexo;
            index = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[index];
            textBox1.Text = row.Cells[0].Value.ToString();
            textBox2.Text = row.Cells[1].Value.ToString();
            textBox3.Text = row.Cells[2].Value.ToString();
            sexo = row.Cells[3].Value.ToString();
            if(sexo == "Feminino")
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.RefreshEdit();
        }
    }
}
