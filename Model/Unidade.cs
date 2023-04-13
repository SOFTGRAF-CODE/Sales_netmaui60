using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace appSGSales2.Model
{
    [Table("CR_UNID")]
    public class Unidade
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string CODUNID { get; set; }
        public string DESCUNID { get; set; }
    }
}

