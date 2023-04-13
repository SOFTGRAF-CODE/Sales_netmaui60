using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace appSGSales2.Model
{
    [Table("CR_PEDE")]
    public class IPedido
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int ID_SEQ { get; set; }
        public int ID_PEDE { get; set; }
        public string  CODMMCC   { get; set; }
        public string DESCRICAO { get; set; }
        public string CODUNID { get; set; }
        public decimal QTDPED    { get; set; }
        public decimal PRECOUNIT { get; set; }
        public decimal TOTAL     { get; set; }
    }
}

