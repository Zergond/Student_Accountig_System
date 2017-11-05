using System.Data.Entity;

namespace DAL.Entities
{
    internal class DBInitializer : CreateDatabaseIfNotExists<AppDBContext>
    {
        protected override void Seed(AppDBContext context)
        {
            base.Seed(context);
        }
    }
}