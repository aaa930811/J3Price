﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace J3Price.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class J3PriceEntities : DbContext
    {
        public J3PriceEntities()
            : base("name=J3PriceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Quotes> Quotes { get; set; }
        public virtual DbSet<RegionMst> RegionMst { get; set; }
        public virtual DbSet<SaleTypeMst> SaleTypeMst { get; set; }
        public virtual DbSet<ServiceMst> ServiceMst { get; set; }
        public virtual DbSet<Exteriors> Exteriors { get; set; }
        public virtual DbSet<Advertisement> Advertisement { get; set; }
    }
}
