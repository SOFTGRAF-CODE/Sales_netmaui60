using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace appSGSales2.Model
{
    [Table("CR_LISTA")]
    public class ListaPreco
    {
        public string CLIENTE { get; set; }
        public string CODMP { get; set; }
        public string DESCRICAO { get; set; }
        public decimal PVENDA { get; set; }
        public DateTime DATAPROC { get; set; }
        public DateTime VALIDADE { get; set; }
        public string STATUS { get; set; }
        public string CODOPE { get; set; }
        public string COD_MPCLI { get; set; }
    }
}
