﻿using ExBuddy.Logging;
using ff14bot.Enums;

namespace ExBuddy.Windows
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.InteropServices;
	using System.Threading.Tasks;

	using ExBuddy.Agents;
	using ExBuddy.Enumerations;
	using ExBuddy.Helpers;

	using ff14bot;
	using ff14bot.Behavior;
	using ff14bot.Managers;

	public sealed class PurifyDialog : Window<PurifyDialog>
	{
		public PurifyDialog() : base("PurifyDialog") { }

		public static async Task<bool> ReduceAllItems(IEnumerable<BagSlot> bagSlots, ushort maxWait = 5000)
		{
			// TODO: Maybe log info why we can't reduce better
			foreach (var bagSlot in bagSlots.Where(bs => bs.IsReducable))
			{
				var result = await CommonTasks.AetherialReduction(bagSlot);
			    if (result.HasFlag(AetherialReductionResult.Failure))
			    {
                    Logger.Instance.Error("An error has occured during aetherial reduction. Result was {0}", result);
                }
                await Behaviors.Sleep(500);
			}

			return true;
		}

		public static async Task<bool> ReduceByItemId(uint itemId, ushort maxWait = 5000)
		{
			return await ReduceAllItems(InventoryManager.FilledSlots.Where(i => i.RawItemId == itemId));
		}

	}
}