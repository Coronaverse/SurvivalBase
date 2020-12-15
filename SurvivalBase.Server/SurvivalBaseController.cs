using JetBrains.Annotations;
using NFive.SDK.Core.Diagnostics;
using NFive.SDK.Server.Communications;
using NFive.SDK.Server.Controllers;
using CRP.SurvivalBase.Server.Models;
using CRP.SurvivalBase.Server.Storage;
using CRP.SurvivalBase.Shared;
using System.Data.Entity.Migrations;
using System;
using System.Linq;

namespace CRP.SurvivalBase.Server
{
	[PublicAPI]
	public class SurvivalBaseController : Controller
	{
		public SurvivalBaseController(ILogger logger, ICommunicationManager comms) : base(logger)
		{
			comms.Event(SurvivalEvents.GetSurvivalObject).FromClients().OnRequest<int>((e, id) => GetSurvival(e, id));
			comms.Event(SurvivalEvents.SyncSurvivalObject).FromClients().OnRequest<Survive>((e, s) => SetSurvival(e, s));
		}

		private void GetSurvival(ICommunicationMessage e, int id)
		{
			Survive s = null;

			using (var context = new StorageContext())
			{
				try
				{
					s = context.Survives.Where(b => b.CharacterId == id).FirstOrDefault();
					if (s == null) throw new System.Exception("Mysql record not found");
				}
				catch
				{
					try
					{
						s = new Survive(id);
						context.Survives.Add(s);
						context.SaveChanges();
					}
					catch (Exception ex)
					{
						this.Logger.Debug(ex.ToString());
					}
				}
			}

			e.Reply(s);
		}

		private void SetSurvival(ICommunicationMessage e, Survive s)
		{
			try
			{
				using (var context = new StorageContext()) context.Survives.AddOrUpdate(s);
			}
			catch (Exception ex)
			{
				this.Logger.Debug(ex.ToString());
			}
		}
	}
}
