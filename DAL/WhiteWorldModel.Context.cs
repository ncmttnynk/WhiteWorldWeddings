﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WhiteWorld.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WhiteWorldEntities : DbContext
    {
        public WhiteWorldEntities()
            : base("name=WhiteWorldEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admin> admin { get; set; }
        public virtual DbSet<anamenu> anamenu { get; set; }
        public virtual DbSet<cms> cms { get; set; }
        public virtual DbSet<etiketler> etiketler { get; set; }
        public virtual DbSet<g_kesim> g_kesim { get; set; }
        public virtual DbSet<g_kumaslar> g_kumaslar { get; set; }
        public virtual DbSet<g_renkler> g_renkler { get; set; }
        public virtual DbSet<g_siluet> g_siluet { get; set; }
        public virtual DbSet<g_yakatipi> g_yakatipi { get; set; }
        public virtual DbSet<gelinlikfotograflari> gelinlikfotograflari { get; set; }
        public virtual DbSet<iletisim> iletisim { get; set; }
        public virtual DbSet<mansetler> mansetler { get; set; }
        public virtual DbSet<sabitler> sabitler { get; set; }
        public virtual DbSet<teksayfalar> teksayfalar { get; set; }
        public virtual DbSet<yorumlar> yorumlar { get; set; }
        public virtual DbSet<gelinlikler> gelinlikler { get; set; }
    }
}
