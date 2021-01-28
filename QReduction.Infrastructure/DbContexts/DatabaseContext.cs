using Microsoft.EntityFrameworkCore;
using QReduction.Core;
using QReduction.Core.Domain;
using QReduction.Infrastructure.DbMappings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QReduction.Infrastructure.DbContexts
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> optionsBuilder)
            : base(optionsBuilder)
        {

        }
        
        #region Overriden
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisterDomainModels(modelBuilder);
            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region Sql Commands

        public int ExecuteQuery(string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            DbConnection cn = this.Database.GetDbConnection();
            DbCommand cmd = cn.CreateCommand();

            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cmd.ExecuteNonQuery();
        }

        public async Task<int> ExecuteQueryAsync(string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            DbConnection cn = this.Database.GetDbConnection();
            DbCommand cmd = cn.CreateCommand();

            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return await cmd.ExecuteNonQueryAsync();
        }

        public IEnumerable<T> QueryFromSql<T>(string commandText, CommandType commandType, params DbParameter[] parameters) where T : class, new()
        {
            using (DbDataReader reader = this.ExecuteDbDataReader(commandText, commandType, parameters))
            {
                if (!reader.HasRows)
                    return null;

                T entity = new T();
                List<T> entities = new List<T>();

                Type entityType = entity.GetType();

                while (reader.Read())
                {
                    entity = new T();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PropertyInfo propertyInfo = entityType.GetProperty(reader.GetName(i));

                        if (propertyInfo != null)
                            propertyInfo.SetValue(entity, reader[i] == DBNull.Value ? null : reader[i]);
                    }
                    entities.Add(entity);
                }

                return entities;
            }
        }

        public async Task<IEnumerable<T>> QueryFromSqlAsync<T>(string commandText, CommandType commandType, params DbParameter[] parameters) where T : class, new()
        {
            using (DbDataReader reader = await ExecuteDbDataReaderAsync(commandText, commandType, parameters))
            {
                if (!reader.HasRows)
                    return null;

                T entity = new T();
                List<T> entities = new List<T>();

                Type entityType = entity.GetType();

                while (reader.Read())
                {
                    entity = new T();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PropertyInfo propertyInfo = entityType.GetProperty(reader.GetName(i));

                        if (propertyInfo != null)
                            propertyInfo.SetValue(entity, reader[i] == DBNull.Value ? null : reader[i]);
                    }
                    entities.Add(entity);
                }

                return entities;
            }
        }

        public DbDataReader ExecuteDbDataReader(string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            DbConnection cn = this.Database.GetDbConnection();
            DbCommand cmd = cn.CreateCommand();

            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cmd.ExecuteReader();
        }

        public Task<DbDataReader> ExecuteDbDataReaderAsync(string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            DbConnection cn = this.Database.GetDbConnection();
            DbCommand cmd = cn.CreateCommand();

            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cmd.ExecuteReaderAsync();
        }

        #endregion


        #region Methods

        private void RegisterDomainModels(ModelBuilder modelBuilder)
        {
            Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == (typeof(CustomEntityTypeConfiguration<>)))
                .ToList()
                .ForEach(map =>
                {
                    if ((Activator.CreateInstance(map) as IModelConfiguration) != null)
                        ((IModelConfiguration)Activator.CreateInstance(map)).ApplyConfiguration(modelBuilder);
                });
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Core.Domain.Job>().HasData(new Core.Domain.Job {Id = (int)Core.Job.BranchReport , Name =$"{Core.Job.BranchReport}"});
        }
        #endregion
    }
}
