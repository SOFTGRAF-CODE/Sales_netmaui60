using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace appSGSales2.Model
{
    [Table("CR_CLI")]
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string CLIENTE { get; set; }
        public string STATUS { get; set; }
        public string RAZAO { get; set; }
        public string ENDERECO { get; set; }
        public string NUMERO { get; set; }
        public string COMPL { get; set; }
        public string BAIRRO { get; set; }
        public string CIDADE { get; set; }
        public string ESTADO { get; set; }
        public string CEP { get; set; }
        public string INSCEST { get; set; }
        public string INDIEDEST { get; set; }
        public string INSCMUN { get; set; }
        public string INSCSUF { get; set; }
        public string CGC { get; set; }
        public string CPF { get; set; }
        public string FONE { get; set; }
        public string DDD { get; set; }
        public string FONE1 { get; set; }
        public string CODVEND { get; set; }
        public string PESSOACONT { get; set; }
        public string CONDPGTO { get; set; }
        public int COND1 { get; set; }
        public int COND2 { get; set; }
        public int COND3 { get; set; }
        public int COND4 { get; set; }
        public int COND5 { get; set; }
        public bool BLOQUEADO { get; set; }
        public string CELULAR { get; set; }
        public string EMAIL { get; set; }
        public int OBSPDV { get; set; }
        public string CODBLOQUE { get; set; }
    }
}

