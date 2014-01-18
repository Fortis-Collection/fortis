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
		internal static ISpawnProvider SpawnProvider;
		public static IItemFactory ItemFactory;

		/// <summary>
		/// Initialise a default Fortis setup with a context provider and verifies the initialisation.
		/// </summary>
		/// <param name="contextProvider"></param>
		public static void Initialise(IContextProvider contextProvider)
		{
			SpawnProvider = new SpawnProvider(
									new TemplateMapProvider(
											new ModelAssemblyProvider()
										)
								);

			ItemFactory = new ItemFactory(contextProvider, SpawnProvider);

			Verify();
		}

		/// <summary>
		/// Initialise Fortis with a spawn provider and item factory and verifies the initialisation.
		/// </summary>
		/// <param name="spawnProvider"></param>
		/// <param name="itemFactory"></param>
		public static void Initialise(ISpawnProvider spawnProvider, IItemFactory itemFactory)
		{
			SpawnProvider = spawnProvider;
			ItemFactory = itemFactory;

			Verify();
		}

		/// <summary>
		/// Verifies that Fortis has been correctly set up.
		/// </summary>
		public static void Verify()
		{
			var verificationFailed = false;
			var errorMessages = new StringBuilder();

			errorMessages.AppendLine("Fortis intialise verification failed:");
			errorMessages.AppendLine();

			if (!VerifySpawnProvider())
			{
				errorMessages.AppendLine("SpawnProvider is not initialised");
				verificationFailed = true;
			}

			if (!VerifyItemFactory())
			{
				errorMessages.AppendLine("ItemFacotry is not initialised");
				verificationFailed = true;
			}

			if (verificationFailed)
			{
				throw new Exception(errorMessages.ToString());
			}
		}

		/// <summary>
		/// Verifies the item factory is correctly set up.
		/// </summary>
		/// <returns></returns>
		public static bool VerifyItemFactory()
		{
			return ItemFactory != null;
		}

		/// <summary>
		/// Verifies the spawn provider is correctly set up.
		/// </summary>
		/// <returns></returns>
		public static bool VerifySpawnProvider()
		{
			return SpawnProvider != null;
		}
	}
}
