using System.Data.Entity;
using NFive.SDK.Core.Models.Player;
using NFive.SDK.Server.Storage;
using CRP.SurvivalBase.Server.Models;

namespace CRP.SurvivalBase.Server.Storage
{
	public class StorageContext : EFContext<StorageContext>
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Survive> Survives { get; set; }
	}
}
