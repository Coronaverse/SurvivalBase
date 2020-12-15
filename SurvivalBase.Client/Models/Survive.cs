using CRP.SurvivalBase.Shared.Models;
using NFive.SDK.Core.Models;
using System;

namespace CRP.SurvivalBase.Client.Models
{
	public class Survive : IdentityModel, ISurvive
	{
		private int _cocaine;
		private int _cocaine_addict;
		private int _alcohol;
		private int _alcohol_addict;
		private int _meth;
		private int _meth_addict;

		public int CharacterId { get; set; }
		public int MaxHealth { get; set; }
		public int Hunger { get; set; }
		public int Armor { get; set; }
		public int Water { get; set; }

		public int Cocaine
		{
			get
			{
				return _cocaine;
			}
			set
			{
				if (value > 100) _cocaine = 100;
				else if (value < -100) _cocaine = -100;
				else _cocaine = value;
			}
		}
		public int CocaineAddict
		{
			get
			{
				return _cocaine_addict;
			}
			set
			{
				if (value > 100) _cocaine_addict = 100;
				if (value < 0) _cocaine_addict = 0;
				else _cocaine_addict = value;
			}
		}
		public int Alcohol
		{
			get
			{
				return _alcohol;
			}
			set
			{
				if (value > 100) _alcohol = 100;
				else if (value < -100) _alcohol = -100;
				else _alcohol = value;
			}
		}
		public int AlcoholAddict
		{
			get
			{
				return _alcohol_addict;
			}
			set
			{
				if (value > 100) _alcohol_addict = 100;
				if (value < 0) _alcohol_addict = 0;
				else _alcohol_addict = value;
			}
		}
		public int Meth
		{
			get
			{
				return _meth;
			}
			set
			{
				if (value > 100) _meth = 100;
				else if (value < -100) _meth = -100;
				else _meth = value;
			}
		}
		public int MethAddict
		{
			get
			{
				return _meth_addict;
			}
			set
			{
				if (value > 100) _meth_addict = 100;
				if (value < 0) _meth_addict = 0;
				else _meth_addict = value;
			}
		}
	}
}
