﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LabSimulation.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LabSimulationDBEntities : DbContext
    {
        public LabSimulationDBEntities()
            : base("name=LabSimulationDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EducationalLevel> EducationalLevels { get; set; }
        public virtual DbSet<EducationalYear> EducationalYears { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<View_EducationalLeve_CountExperience> View_EducationalLeve_CountExperience { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<View_ExperienceInfo> View_ExperienceInfo { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserTypeAccount> UserTypeAccounts { get; set; }
        public virtual DbSet<View_User> View_User { get; set; }
        public virtual DbSet<View_Comments_Users> View_Comments_Users { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
    }
}
