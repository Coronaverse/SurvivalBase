using System;
using JetBrains.Annotations;
using NFive.SDK.Core.Models;

namespace CRP.SurvivalBase.Shared.Models
{
	public interface ISurvive : IIdentityModel
	{
		int CharacterId { get; set; }
		int MaxHealth { get; set; }
		int Hunger { get; set; }
		int Armor { get; set; }
		int Water { get; set; }

		int Cocaine { get; set; }
		int CocaineAddict { get; set; }
		int Alcohol { get; set; }
		int AlcoholAddict { get; set; }
		int Meth { get; set; }
		int MethAddict { get; set; }
	}
}
