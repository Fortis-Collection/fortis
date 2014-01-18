using Fortis.Model;
using Fortis.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis
{
	public static class Global
	{
		public static ISpawnProvider SpawnProvider;
		public static IItemFactory ItemFactory;

		public static void Initialise(ISpawnProvider spawnProvider, IItemFactory itemFactory)
		{
			SpawnProvider = spawnProvider;
			ItemFactory = itemFactory;

			Verify();
		}

		public static void Verify()
		{
			var verificationFailed = false;
			var errorMessages = new StringBuilder();

			errorMessages.AppendLine("Fortis intialise verification failed:");
			errorMessages.AppendLine();

			if (SpawnProvider == null)
			{
				errorMessages.AppendLine("SpawnProvider is not initialised");
				verificationFailed = true;
			}

			if (ItemFactory == null)
			{
				errorMessages.AppendLine("ItemFacotry is not initialised");
				verificationFailed = true;
			}

			if (verificationFailed)
			{
				throw new Exception(errorMessages.ToString());
			}
		}
	}
}
