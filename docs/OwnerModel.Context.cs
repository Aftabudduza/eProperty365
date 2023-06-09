﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PropertyService.BO
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Core.EntityClient;
    using System.Data.Entity.Core.Mapping;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;
    using System.Data.Entity.Infrastructure;

    internal partial class EPropertyEntities : DbContext
    {
        public EPropertyEntities()
            : base("name=EPropertyEntities")
        {
        }

        
        public EPropertyEntities(EntityConnection entityConnection) :
                            base(entityConnection, false)
        {

        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Child> Child { get; set; }
        public virtual DbSet<Communication> Communication { get; set; }
        public virtual DbSet<ContactInformation> ContactInformation { get; set; }
        public virtual DbSet<Master> Master { get; set; }
        public virtual DbSet<OwnerProfile> OwnerProfile { get; set; }
        public virtual DbSet<PaymentInformation> PaymentInformation { get; set; }
        public virtual DbSet<SystemInformation> SystemInformation { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
    }
    internal static class DatabaseUtils
    {
        /// <summary>
        /// Builds the connection string for Entity framework.
        /// </summary>
        /// <returns></returns>
        public static EntityConnection BuildConnection(BuildConnectionParams buildConnectionParams)
        {
            var sqlBuilder = new SqlConnectionStringBuilder
            {
                DataSource = buildConnectionParams.ServerName,
                InitialCatalog = buildConnectionParams.DatabaseName,
                IntegratedSecurity = true
            };

            string sMeta = string.Format(@"res://*/{0}.csdl|
							res://*/{0}.ssdl|
							res://*/{0}.msl", buildConnectionParams.ModelName);

            var providerString = sqlBuilder.ToString();
            var entityBuilder = new EntityConnectionStringBuilder
            {
                Provider = buildConnectionParams.ProviderName,
                ProviderConnectionString = providerString,
                Metadata = sMeta
            };

            return CreateConnection(buildConnectionParams.SchemaName, entityBuilder, buildConnectionParams.ModelName);
        }

        /// <summary>
        /// Creates the EntityConnection, based on new schema & existing connectionString
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="connectionBuilder"></param>
        /// <param name="modelName">Name of the model.</param>
        /// <returns></returns>
        private static EntityConnection CreateConnection(string schemaName, EntityConnectionStringBuilder connectionBuilder, string modelName)
        {
            Func<string, Stream> generateStream =
                extension => Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Concat(modelName, extension));

            Action<IEnumerable<Stream>> disposeCollection = streams =>
            {
                if (streams == null)
                    return;

                foreach (var stream in streams.Where(stream => stream != null))
                    stream.Dispose();
            };

            var conceptualReader = generateStream(".csdl");
            var mappingReader = generateStream(".msl");
            var storageReader = generateStream(".ssdl");

            if (conceptualReader == null || mappingReader == null || storageReader == null)
            {
                disposeCollection(new[] { conceptualReader, mappingReader, storageReader });
                return null;
            }

            var storageXml = XElement.Load(storageReader);

            foreach (var entitySet in storageXml.Descendants())
            {
                var schemaAttribute = entitySet.Attributes("Schema").FirstOrDefault();
                if (schemaAttribute != null)
                    schemaAttribute.SetValue(schemaName);
            }

            storageXml.CreateReader();

            var workspace = new MetadataWorkspace();

            var storageCollection = new StoreItemCollection(new[] { storageXml.CreateReader() });
            var conceptualCollection = new EdmItemCollection(new[] { XmlReader.Create(conceptualReader) });
            var mappingCollection = new StorageMappingItemCollection(conceptualCollection,
                                                                    storageCollection,
                                                                    new[] { XmlReader.Create(mappingReader) });

            workspace.RegisterItemCollection(conceptualCollection);
            workspace.RegisterItemCollection(storageCollection);
            workspace.RegisterItemCollection(mappingCollection);

            var connection = DbProviderFactories.GetFactory(connectionBuilder.Provider).CreateConnection();
            if (connection == null)
            {
                disposeCollection(new[] { conceptualReader, mappingReader, storageReader });
                return null;
            }

            connection.ConnectionString = connectionBuilder.ProviderConnectionString;
            return new EntityConnection(workspace, connection);
        }
    }

    internal partial class EPropertyEntities : IDisposable
    {
        private readonly string OwnerModel = Utility.ModelName; // EDMX MODEL!

        private EPropertyEntities _entities;

       
        //public EPropertyEntities()
        //{
        //    var entityConnection = DatabaseUtils.BuildConnection(new BuildConnectionParams
        //    {
        //        ProviderName = "System.Data.SqlClient",
        //        ServerName = Utility.Server,
        //        DatabaseName = Utility.Database,
        //        ModelName = OwnerModel,
        //        SchemaName = "UserProfile"
        //    });

        //    if (entityConnection == null)
        //        throw new Exception("Can't create EntityConnection");

        //    _entities = new EPropertyEntities(entityConnection);
        //}
        public void Dispose()
        {
            if (_entities == null)
                return;

            _entities.Dispose();
            _entities = null;
        }


    }

    internal struct BuildConnectionParams
    {
        public string ProviderName
        {
            get;
            set;
        }

        public string ServerName
        {
            get;
            set;
        }

        public string DatabaseName
        {
            get;
            set;
        }

        public string ModelName
        {
            get;
            set;
        }

        public string SchemaName
        {
            get;
            set;
        }
    }
}
