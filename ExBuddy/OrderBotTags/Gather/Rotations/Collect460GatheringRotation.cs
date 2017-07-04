﻿namespace ExBuddy.OrderBotTags.Gather.Rotations
{
	using Attributes;
	using ff14bot;
	using Interfaces;
	using System.Threading.Tasks;

	[GatheringRotation("Onix460", 33, 600)]
	public sealed class Collect460GatheringRotation : CollectableGatheringRotation, IGetOverridePriority
	{
		#region IGetOverridePriority Members

		int IGetOverridePriority.GetOverridePriority(ExGatherTag tag)
		{
			// if we have a collectable && the collectable value is greater than or equal to 460: Priority 460
			if (tag.CollectableItem != null && tag.CollectableItem.Value >= 460)
			{
				return 460;
			}
			return -1;
		}

		#endregion IGetOverridePriority Members

		public override async Task<bool> ExecuteRotation(ExGatherTag tag)
		{
			if (tag.IsUnspoiled())
			{
				await SingleMindMethodical(tag);
				await SingleMindMethodical(tag);
				await UtmostCaution(tag);
				await Methodical(tag);
#if RB_CN
				await UtmostCaution(tag);
#endif
				await Methodical(tag);
				await IncreaseChance(tag);
			}
			else
			{
#if RB_CN
				if (Core.Player.CurrentGP >= 600 && tag.GatherItem.Chance < 98)
				{
#else
				if (Core.Player.CurrentGP >= 600)
				{
#endif
					await SingleMindMethodical(tag);
					await SingleMindMethodical(tag);
					await UtmostCaution(tag);
					await Methodical(tag);
#if RB_CN
				await UtmostCaution(tag);
#endif
					await Methodical(tag);
					await IncreaseChance(tag);
					return true;
				}

				await Impulsive(tag);
				await Impulsive(tag);
				await Methodical(tag);
			}
			return true;
		}
	}
}