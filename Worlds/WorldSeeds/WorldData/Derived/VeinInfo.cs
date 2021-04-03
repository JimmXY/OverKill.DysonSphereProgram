namespace WorldData.Derived
{
    public class VeinInfo
	{
		public int TotalCount { get; set; }

		public int Veins { get; set; }

		public RatingTier Tier { get; set; }

		public string TierSingle => Tier.ToString().Replace("_TIER", "");

		public static (RatingTier Tier, int RangeStart, int RangeEnd)[] TierRanges = new[]
		{
		(RatingTier.S_TIER, 10_000_000, 100_000_000),
		(RatingTier.A_TIER, 2_000_000, 10_000_000),
		(RatingTier.B_TIER, 1_000_000, 2_000_000),
		(RatingTier.C_TIER, 500_000, 1_000_000),
		(RatingTier.D_TIER, 100_000, 500_000),
		(RatingTier.E_TIER, 0, 100_000),
		(RatingTier.X_TIER, -100_000_000, 0)
	};

		public VeinInfo(int totalCount, int veins)
		{
			TotalCount = totalCount;
			Veins = veins;
			Tier = TierRanges.First(f => totalCount > f.RangeStart && totalCount <= f.RangeEnd).Tier;
		}

		public VeinInfo(params string[] columns)
			: this(int.TryParse(columns[0], out int totalCount) ? totalCount : -1,
			int.TryParse(columns[1], out int veins) ? veins : -1)
		{ }
	}
}
