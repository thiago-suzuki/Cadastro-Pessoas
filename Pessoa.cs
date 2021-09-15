using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPessoasSQL
{
    class Pessoa
    {
        private String id;
        private String nome;
        private String idade;
        private String sexo;

        public String getID() { return id; }
        public String getNome() { return nome; }
        public String getIdade() { return idade; }
        public String getSexo() { return sexo; }

        public void setID(String id) { this.id = id; }
        public void setNome(String nome) { this.nome = nome; }
        public void setIdade(String idade) { this.idade = idade; }
        public void setSexo(String sexo) { this.sexo = sexo; }



    }
}
