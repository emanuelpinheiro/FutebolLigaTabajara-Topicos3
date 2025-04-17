using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;

namespace FutebolLigaTabajara
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Registro de áreas, filtros, rotas e bundles
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Configurar o inicializador do banco de dados
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LigaDBContext, Migrations.Configuration>());

            // Inicializar o banco de dados
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var context = new LigaDBContext())
            {
                // Chama o método Seed para popular o banco de dados
                Console.WriteLine("Inicializando o banco de dados...");
                LigaDBInitializer.Seed(context);
                context.SaveChanges();
                Console.WriteLine("Banco de dados inicializado com sucesso.");
            }
        }
    }
}
