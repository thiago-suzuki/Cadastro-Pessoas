using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaErro;
using System.Data;

namespace CadastroPessoasSQL
{
    class PessoaBLL
    {
        public static void Conectar() { PessoaDAL.Conectar(); }
        public static void Desconectar() { PessoaDAL.Desconectar(); }
        public static DataTable getDT() { return PessoaDAL.getDT(); }
        public static void populaDR() { PessoaDAL.populaDR(); }
        public static Pessoa getPrimeiro() { return PessoaDAL.getPrimeiro(); }
        public static Pessoa getAnterior() { return PessoaDAL.getAnterior(); }
        public static Pessoa getProximo() { return PessoaDAL.getProximo(); }
        public static Pessoa getUltimo() {  return PessoaDAL.getUltimo(); }
        public static void ValidaCodigo(Pessoa pessoa, char op)
        {
            Erro.setErro(false);
            if (pessoa.getID().Equals(""))
            {
                Erro.setErro("O ID é de Preenchimento Obrigatório!");
                return;
            }
            if (op == 'c')
                PessoaDAL.ConsultarPessoa(pessoa);
            else
                PessoaDAL.ExcluirPessoa(pessoa);
        }
        public static void ValidaDados(Pessoa pessoa, char op)
        {
            Erro.setErro(false);
            if (pessoa.getID().Equals(""))
            {
                Erro.setErro("O ID é de Preenchimento Obrigatório!");
                return;
            }
            
            if (pessoa.getNome().Equals(""))
            {
                Erro.setErro("O nome é de Preenchimento Obrigatório!");
                return;
            }

            if (pessoa.getIdade().Equals(""))
            {
                Erro.setErro("A idade é de Preenchimento Obrigatório!");
                return;
            }
            else
            {
                try { int.Parse(pessoa.getIdade()); }
                catch { Erro.setErro("A idade tem que ser Numérica!"); return; }
                if (int.Parse(pessoa.getIdade()) <= 0)
                {
                    Erro.setErro("A idade tem que ser Maior que Zero!");
                    return;
                }
            }

            if (pessoa.getSexo().Equals(""))
            {
                Erro.setErro("O sexo tem que ser informado!");
                return;
            }

            if (op == 'a')
                PessoaDAL.AtualizarPessoa(pessoa);
            else
                PessoaDAL.InserirPessoa(pessoa);
        }
    }
}
