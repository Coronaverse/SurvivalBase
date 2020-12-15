using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFive.SDK.Core.Models;
using CRP.SurvivalBase.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRP.SurvivalBase.Server.Models
{
	public class Survive : IdentityModel, ISurvive
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Index(IsUnique = true)]
		public int CharacterId { get; set; }

		[Required]
		public int MaxHealth { get; set; }

		[Required]
		public int Hunger { get; set; }

		[Required]
		public int Armor { get; set; }

		[Required]
		public int Water { get; set; }

		[Required]
		public int Cocaine { get; set; }

		[Required]
		public int CocaineAddict { get; set; }

		[Required]
		public int Alcohol { get; set; }

		[Required]
		public int AlcoholAddict { get; set; }

		[Required]
		public int Meth { get; set; }

		[Required]
		public int MethAddict { get; set; }

		public Survive() { }

		public Survive(int id)
		{
			this.CharacterId = id;
		}
	}
}
