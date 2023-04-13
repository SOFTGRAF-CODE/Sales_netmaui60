using SQLite;

namespace appSGSales2.Model
{
    [Table("Pedido")]
    public class Pedido
    {
        [PrimaryKey,AutoIncrement]
        public int id_numpede_app { get; set; }
        public int numpede { get; set; }
        public int id_imei { get; set; }
        public int numped { get; set; }
        public string codvend { get; set; }
        //public string codcli { get; set; }
        public string   CODCLI  { get; set; }
        public DateTime DTEMISS { get; set; }
        public string   CONDPGTO { get; set; }
        public string   CONTATO  { get; set; }
        public string   TRANSPORTADORA { get; set; }
        public string   OBSERVACAO { get; set; }
        public bool IsSelected { get; set; }
        public bool IsSend { get; set; }
    }
}
