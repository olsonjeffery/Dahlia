using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Dahlia.Services;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Generators;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SqlServer;

namespace Dahlia.Configuration.Persistence.Migrations
{
    public class SqlMigrationService : IMigrationService
    {
        
       MigrationRunner getRunner()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["dahliaSQL"].ConnectionString;
            var connection = new SqlConnection(connectionString);
            var assembly = typeof(SqlMigrationService).Assembly;
            var ns = typeof(SqlMigrationService).Namespace;
            var generator = new SqlServer2005Generator();
            var processor = new SqlServerProcessor(connection, generator, new NullAnnouncer(), new ProcessorOptions());

            return new MigrationRunner(assembly, new RunnerContext(new NullAnnouncer()) { Namespace = ns },
                                          processor);
        }

        public void MigrateUp(long? version)
        {
            if(version.HasValue)
                getRunner().MigrateUp(version.Value);
            else
                getRunner().MigrateUp();
        }

        public void MigrateDown(long version)
        {
            getRunner().MigrateDown(version);
        }
    }
}