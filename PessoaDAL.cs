using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaErro;
using System.Data.OleDb;
using System.Data;

namespace CadastroPessoasSQL
{
    class PessoaDAL
    {
        private static String strconexao = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =Cadastro.mdb";
        private static OleDbConnection conn = new OleDbConnection(strconexao);
        private static OleDbCommand strSQL;
        private static OleDbDataReader result;
        private static OleDbDataAdapter adaptador;
        private static DataTable dt = new DataTable();
        private static DataTable dt2 = new DataTable();
        private static int i = -1;
        private static Pessoa pessoa = new Pessoa();

        public static void Conectar()
        {
            try 
            { 
                conn.Open(); 
            }
            catch (Exception) 
            { 
                Erro.setErro("Problemas ao se Conectar ao Banco de Dados!"); 
            }
        }

        public static void Desconectar()
        {
            conn.Close();
        }

        public static DataTable getDT()
        {
            Conectar();
            String aux = "select * from TabPessoas";
            adaptador = new OleDbDataAdapter(aux, conn);
            adaptador.Fill(dt);
            adaptador.Dispose();
            Desconectar();
            return dt;
        }

        public static void populaDR()
        {
            Conectar();
            String aux = "select * from TabPessoas";
            adaptador = new OleDbDataAdapter(aux, conn);
            adaptador.Fill(dt2);
            adaptador.Dispose();
            Desconectar();
        }

        public static void setPessoa()
        {
            pessoa.setID(dt.Rows[i].ItemArray[0].ToString());
            pessoa.setNome(dt.Rows[i].ItemArray[1].ToString());
            pessoa.setIdade(dt.Rows[i].ItemArray[2].ToString());
            pessoa.setSexo(dt.Rows[i].ItemArray[3].ToString());
        }

        public static Pessoa getProximo()
        {
            Erro.setErro(false);
            try
            {
                i++;
                setPessoa();
            }
            catch (Exception)
            {
                Erro.setErro("Não é possível avançar a Direita!");
                i = dt.Rows.Count - 1;
            }
            return pessoa;
        }

        public static Pessoa getAnterior()
        {
            Erro.setErro(false);
            try
            {
                i--;
                setPessoa();
            }
            catch (Exception)
            {
                Erro.setErro("Não é possível avançar a Esquerda!");
                i = -1;
            }
            return pessoa;
        }

        public static Pessoa getPrimeiro()
        {
            i = 0;
            setPessoa();
            return pessoa;
        }

        public static Pessoa getUltimo()
        {
            i = dt.Rows.Count - 1;
            setPessoa();
            return pessoa;
        }

        public static void InserirPessoa(Pessoa pessoa)
        {
            Conectar();
            String aux = "insert into TabPessoas(id,nome,idade,sexo) values (@id,@nome,@idade,@sexo)";
            strSQL = new OleDbCommand(aux, conn);
            strSQL.Parameters.Add("@id", OleDbType.Integer).Value = pessoa.getID();
            strSQL.Parameters.Add("@nome", OleDbType.VarChar).Value = pessoa.getNome();
            strSQL.Parameters.Add("@idade", OleDbType.Integer).Value = pessoa.getIdade();
            strSQL.Parameters.Add("@sexo", OleDbType.VarChar).Value = pessoa.getSexo();
            try { strSQL.ExecuteNonQuery(); }
            catch(Exception) { Erro.setErro("Chave Duplicada!"); }
            Desconectar();
        }
        public static void ExcluirPessoa(Pessoa pessoa)
        {
            Conectar();
            String aux = "delete from TabPessoas where id = @id";
            strSQL = new OleDbCommand(aux, conn);
            strSQL.Parameters.Add("@id", OleDbType.Integer).Value = pessoa.getID();
            strSQL.ExecuteNonQuery();
            Desconectar();
        }
        public static void AtualizarPessoa(Pessoa pessoa)
        {
            Conectar();
            String aux = "update TabPessoas set nome=@nome, idade=@idade, sexo=@sexo where id =@id";
            strSQL = new OleDbCommand(aux, conn);
            strSQL.Parameters.Add("@nome", OleDbType.VarChar).Value = pessoa.getNome();
            strSQL.Parameters.Add("@idade", OleDbType.Integer).Value = pessoa.getIdade();
            strSQL.Parameters.Add("@sexo", OleDbType.VarChar).Value = pessoa.getSexo();
            strSQL.Parameters.Add("@id", OleDbType.Integer).Value = pessoa.getID();
            strSQL.ExecuteNonQuery();
            Desconectar();
        }
        public static void ConsultarPessoa(Pessoa pessoa)
        {
            Conectar();
            String aux = "select * from TabPessoas where id = @id";
            strSQL = new OleDbCommand(aux, conn);
            strSQL.Parameters.Add("@id", OleDbType.Integer).Value = pessoa.getID();
            result = strSQL.ExecuteReader();
            Erro.setErro(false);
            if (result.Read())
            {
                pessoa.setNome(result.GetString(1));
                pessoa.setIdade(result.GetValue(2).ToString());
                pessoa.setSexo(result.GetString(3));
            }
            else
            {
                Erro.setErro("Pessoa não Cadastrada");
            }
            Desconectar();
        }
    }
}
