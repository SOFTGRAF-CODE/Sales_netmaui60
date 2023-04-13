using SQLite;
using appSGSales2.Model;

namespace appSGSales2.Database
{
    public class GerenciadorDB
    {
        SQLiteAsyncConnection _conexao;

        public GerenciadorDB()
        {
        }

        async Task Init()
        {
            if (_conexao is not null)
                return;

            _conexao = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await _conexao.CreateTableAsync<Pedido>();
            await _conexao.CreateTableAsync<IPedido>();
            await _conexao.CreateTableAsync<Cliente>();
            await _conexao.CreateTableAsync<Unidade>();
            await _conexao.CreateTableAsync<ListaPreco>();

        }
        public async Task<List<Pedido>> ConsultarPedido()
        {
            await Init();
            string cCodVend = Preferences.Default.Get("CODVEND", "");
            return await _conexao.Table<Pedido>().Where(b=> (b.IsSend.Equals(false)) && (b.codvend == cCodVend)).OrderByDescending(a => a.DTEMISS).ToListAsync();
        }

        public async Task<List<Pedido>> ConsultarPedidos(string codvend)
        {
            await Init();
            return await _conexao.Table<Pedido>().Where(a=> a.codvend == codvend).ToListAsync();
        }

        public async Task<Pedido> ObterPedidoPorId(int id)
        {
            await Init();
            return await _conexao.Table<Pedido>().Where(a=> a.id_numpede_app == id).FirstOrDefaultAsync();
        }

        public async Task inserePedido(Pedido pedido)
        {
            await Init();
            await _conexao.InsertAsync(pedido);
        }

        public async void envioStatusPedido(Pedido pedido)
        {
            await Init();
            await _conexao.UpdateAsync(pedido);
            /*
            try
            {
                _conexao.BeginTransaction();

                StringBuilder sbUpdate = new StringBuilder();

                Pedido _pedido = _conexao.Query<Pedido>("select * from pedido where id_numpede_app = ? and codvend=? and DTEMISS=?", pedido.id_numpede_app,pedido.codvend,pedido.DTEMISS).FirstOrDefault();
                //Pedido _pedido = _conexao.Query<Pedido>("select * from pedido where id_numpede_app = ? and codvend=?", pedido.id_numpede_app, pedido.codvend).FirstOrDefault();
                if (_pedido != null)
                {
                    _pedido.IsSend = true;
                   _conexao.Update(_pedido);
                   _conexao.Commit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                _conexao.Rollback();
            }
            */
        }

        public async Task atualizaPedido(Pedido pedido)
        {
            await Init();
            await _conexao.UpdateAsync(pedido);
        }

        public async Task exclusaoPedido(Pedido pedido)
        {
            await Init();
            List<IPedido> ipedidoAll = await _conexao.Table<IPedido>().Where(a => a.ID_PEDE == pedido.numpede).ToListAsync();
            await _conexao.DeleteAsync(pedido);
            foreach (var item in ipedidoAll)
            {
                await _conexao.DeleteAsync(item);
            }
        }

        public async Task insereItemPedido(IPedido ipedido)
        {
            await Init();
            await _conexao.InsertAsync(ipedido);
        }

        public async Task atualizaItemPedido(IPedido ipedido)
        {
            await Init();
            await _conexao.UpdateAsync(ipedido);
        }

        public async Task exclusaoItemPedido(IPedido ipedido)
        {
            await Init();
            await _conexao.DeleteAsync(ipedido);
        }

        public async Task<List<IPedido>> consultaItensPedido(int id)
        {
            await Init();
            return await _conexao.Table<IPedido>().Where(a => a.ID_PEDE == id).ToListAsync();
        }

        public async Task<List<Unidade>> consultaUnidades(string cFiltro)
        {
            List<Unidade> _unidade = await _conexao.Table<Unidade>().Where(a => a.CODUNID.ToUpper().Contains(cFiltro.ToUpper()) || a.DESCUNID.ToUpper().Contains(cFiltro.ToUpper())).ToListAsync();
            return _unidade;
        }

        public async void insereCliente(List<Cliente> _listaCliente )
        {
            await Init();

            deletaClientes();

            foreach (Cliente _cliente in _listaCliente)
            {
                await _conexao.InsertAsync(_cliente);
            }
        }

        public async void insereUnidade(List<Unidade> _listaUnidade)
        {
            await Init();
            //await deletaUnidades();

            //_conexao.BeginTransaction();
            foreach (Unidade _unidade in _listaUnidade)
            {
                await _conexao.InsertAsync(_unidade);
            }
            //_conexao.Commit();
        }
        /*
        public void deletaUnidades()
        {
            try
            {
                //_conexao.BeginTransaction();
                //var comando = _conexao.CreateCommand("DELETE FROM CR_UNID");
                //comando.ExecuteNonQuery();
                //_conexao.Commit();
            }
            catch (Exception)
            {
                //_conexao.Rollback();
            }
        }

        public void insereListaPreco(List<ListaPreco> _listaPreco)
        {
            deletaLista();

            _conexao.BeginTransaction();
            foreach (ListaPreco _preco in _listaPreco)
            {
                _conexao.Insert(_preco);
            }
            _conexao.Commit();
        }

        public void deletaLista()
        {
            try
            {
                _conexao.BeginTransaction();
                var comando = _conexao.CreateCommand("DELETE FROM CR_LISTA");
                comando.ExecuteNonQuery();
                _conexao.Commit();
            }
            catch (Exception)
            {
                _conexao.Rollback();
            }
        }

        */
        async void deletaClientes()
        {
            try
            {
                await _conexao.ExecuteAsync("DELETE FROM CR_CLI");
            }
            catch (Exception)
            {
            }
        }

        public async Task<List<Cliente>> consultaClientes(string cFiltro)
        {
            await Init();
            
            //List<Cliente> cliente = await _conexao.Table<Cliente>().Where(a => a.CLIENTE.ToUpper().Contains(cFiltro.ToUpper()) || a.RAZAO.ToUpper().Contains(cFiltro.ToUpper()) || a.ENDERECO.ToUpper().Contains(cFiltro.ToUpper()) || a.CIDADE.ToUpper().Contains(cFiltro.ToUpper()) || a.CGC.ToUpper().Contains(cFiltro.ToUpper()) || a.BAIRRO.ToUpper().Contains(cFiltro.ToUpper())).ToListAsync();

            if (string.IsNullOrWhiteSpace(cFiltro))
              await _conexao.Table<Cliente>().ToListAsync();

            return await _conexao.QueryAsync<Cliente>(@"
                SELECT Cliente, Razao,Endereco,Bairro,Estado,CGC,CPF
                FROM cr_cli
                WHERE 
                    Cliente LIKE ? OR
                    Razao LIKE ? OR
                    Endereco LIKE ? OR
                    CGC LIKE ? OR
                    Bairro LIKE ? OR
                    Cidade LIKE ?",
                    $"{cFiltro}%",
                    $"{cFiltro}%",
                    $"{cFiltro}%",
                    $"{cFiltro}%",
                    $"{cFiltro}%",
                    $"{cFiltro}%");
        }

        /* public void insereProdutos()
         {
             StringBuilder sbInsert = new StringBuilder();
             sbInsert.Append("INSERT INTO CR_MP(CODMP,DESCRICAO) VALUES ('PCP110BACM','PRATICUP P1 100ML - BACIO DI LATTER PROMO'),");
             sbInsert.Append("('PCP110BACN','PCP1 100ML BACIO - DIAS DOS NAMORADOS'),");
             sbInsert.Append("('PCP110BACR','EMB. PCP1 100ML BACIO DI LATTE DIA CRIAN');");
             _conexao.BeginTransaction();
             var comando = _conexao.CreateCommand(sbInsert.ToString());
             comando.ExecuteNonQuery();
             _conexao.Commit();
         }
        */

        public async Task<List<ListaPreco>> consultaProdutos(string cCodCli, string cFiltro)
        {
            //string cSelect = String.Format("SELECT * FROM CR_CLI WHERE CODCLI LIKE '%{0}%'", CODCLI);
            //string cSelect = "SELECT * FROM CR_CLI";
            //var comando = _conexao.CreateCommand(cSelect);
            string cFiltroUpper = cFiltro.ToUpper();
            List<ListaPreco> produto = await _conexao.Table<ListaPreco>().Where(a => a.CLIENTE.ToUpper().Contains(cCodCli.ToUpper()) && ( a.CODMP.ToUpper().Contains(cFiltroUpper) || a.DESCRICAO.ToUpper().Contains(cFiltroUpper)) ).ToListAsync();
            return produto;

        }


    }
}

