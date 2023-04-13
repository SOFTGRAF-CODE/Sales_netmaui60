using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using appSGSales2.Model;
using appSGSales2.Database;
using System.Reflection;
using appSGSales2.Service;
using System.Net.Http;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace appSGSales2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnvioPedidos : TabbedPage
    {
        public EnvioPedidos()
        {
            InitializeComponent();
        }

    }
}