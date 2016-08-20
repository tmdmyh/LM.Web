// -----------------------------------------------------------------------
//  <copyright file="DbContextInitializerBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-28 16:07</last-date>
// -----------------------------------------------------------------------

using LM.Core.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;




namespace LM.Core.Data.Initializers
{
    /// <summary>
    /// 数据上下文初始化基类
    /// </summary>
    public abstract class DbContextInitializerBase<TDbContext>
        
        where TDbContext : DbContext, new()
    {
        /// <summary>
        /// 初始化一个<see cref="DbContextInitializerBase"/>类型的新实例
        /// </summary>
        protected DbContextInitializerBase()
        {
            CreateDatabaseInitializer = new CreateDatabaseIfNotExists<TDbContext>();
            MigrateInitializer = new MigrateDatabaseToLatestVersion<TDbContext, AutoMigrationsConfiguration<TDbContext>>();
        }

        /// <summary>
        /// 获取或设置 设置数据库创建初始化，默认为<see cref="CreateDatabaseIfNotExists{TDbContext}"/>
        /// </summary>
        public IDatabaseInitializer<TDbContext> CreateDatabaseInitializer { get; set; }

        /// <summary>
        /// 获取或设置 数据迁移策略，默认为自动迁移
        /// </summary>
        public IDatabaseInitializer<TDbContext> MigrateInitializer { get; set; }



        /// <summary>
        /// 数据库初始化实现，设置数据库初始化策略，并进行EntityFramework的预热
        /// </summary>
        public void ContextInitialize()
        {
            TDbContext context = new TDbContext();
            IDatabaseInitializer<TDbContext> initializer;
            if (!context.Database.Exists())
            {
                initializer = CreateDatabaseInitializer;
            }
            else
            {
                initializer = MigrateInitializer;
            }
            Database.SetInitializer(initializer);

            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
            StorageMappingItemCollection mappingItemCollection = (StorageMappingItemCollection)objectContext.ObjectStateManager
                .MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
            mappingItemCollection.GenerateViews(new List<EdmSchemaError>());
            context.Dispose(); 
        }
    }


}