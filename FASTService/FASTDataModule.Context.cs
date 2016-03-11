﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FASTService
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FASTDBEntities : DbContext
    {
        public FASTDBEntities()
            : base("name=FASTDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AccessRight> AccessRights { get; set; }
        public DbSet<AssetAssignment> AssetAssignments { get; set; }
        public DbSet<AssetClass> AssetClasses { get; set; }
        public DbSet<AssetStatu> AssetStatus { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<AssignmentStatu> AssignmentStatus { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<FixAsset> FixAssets { get; set; }
        public DbSet<Issuer> Issuers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ReasonCode> ReasonCodes { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<vwAssetAssignment> vwAssetAssignments { get; set; }
        public DbSet<vwAssetAssignmentsForCustodian> vwAssetAssignmentsForCustodians { get; set; }
        public DbSet<vwAssetAssignmentsForManager> vwAssetAssignmentsForManagers { get; set; }
        public DbSet<vwAssetAssignmentsForMI> vwAssetAssignmentsForMIS { get; set; }
        public DbSet<vwCustodiansList> vwCustodiansLists { get; set; }
        public DbSet<vwDepartmentList> vwDepartmentLists { get; set; }
        public DbSet<vwEmployeeList> vwEmployeeLists { get; set; }
        public DbSet<vwFixAssetList> vwFixAssetLists { get; set; }
        public DbSet<vwManagersList> vwManagersLists { get; set; }
        public DbSet<vwMISList> vwMISLists { get; set; }
        public DbSet<AccessLevel> AccessLevels { get; set; }
    }
}
