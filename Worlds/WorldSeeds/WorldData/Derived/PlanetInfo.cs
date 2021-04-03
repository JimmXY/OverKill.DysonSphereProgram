using System;
using System.Linq;

using WorldData.Defined;

namespace WorldData.Derived
{
    public class PlanetInfo
	{
		public string Name => _row[NamedColumnIndices.PlanetName];

		public string Parent => _row[NamedColumnIndices.OrbitParent];

		public bool IsGiant => Environment.Contains("giant", StringComparison.CurrentCultureIgnoreCase);

		public int SAT => IsGiant ? Moons.Length : 0;

		public PlanetInfo[] Moons { get; set; } = new PlanetInfo[0];

		public decimal DistanceToStar { get; set; }

		public VeinInfo Iron => new VeinInfo(_row[NamedColumnIndices.IronCount], _row[NamedColumnIndices.IronVeins]);

		public VeinInfo FireIce => new VeinInfo(_row[NamedColumnIndices.FireIceCount], _row[NamedColumnIndices.FireIceVeins]);

		public VeinInfo Copper => new VeinInfo(_row[NamedColumnIndices.CopperCount], _row[NamedColumnIndices.CopperVeins]);

		public VeinInfo Silicon => new VeinInfo(_row[NamedColumnIndices.SiliconCount], _row[NamedColumnIndices.SiliconVeins]);

		public VeinInfo Titanium => new VeinInfo(_row[NamedColumnIndices.TitaniumCount], _row[NamedColumnIndices.TitaniumVeins]);

		public VeinInfo Stone => new VeinInfo(_row[NamedColumnIndices.StoneCount], _row[NamedColumnIndices.StoneVeins]);

		public VeinInfo Coal => new VeinInfo(_row[NamedColumnIndices.CoalCount], _row[NamedColumnIndices.CoalVeins]);

		public VeinInfo OrganicCrystal => new VeinInfo(_row[NamedColumnIndices.OrganicCrystalCount], _row[NamedColumnIndices.OrganicCrystalVeins]);

		public VeinInfo Spiniform => new VeinInfo(_row[NamedColumnIndices.SpiniformCount], _row[NamedColumnIndices.SpiniformVeins]);

		public VeinInfo OpticalGrating => new VeinInfo(_row[NamedColumnIndices.OpticalGratingCount], _row[NamedColumnIndices.OpticalGratingVeins]);

		public VeinInfo Unipolar => new VeinInfo(_row[NamedColumnIndices.UnipolarCount], _row[NamedColumnIndices.UnipolarVeins]);

		public bool HasSulfuricAcid => _row[NamedColumnIndices.OceanType] == "Sulfuric acid";

		public bool HasWater => _row[NamedColumnIndices.OceanType] == "Water";

		public string Environment => _row[NamedColumnIndices.PlanetType];

		public decimal CrudeOil => decimal.Parse(_row[NamedColumnIndices.CrudeOilSeep]);

		public decimal Hydrogen => decimal.Parse(_row[NamedColumnIndices.Hydrogen]);

		public decimal FireIceGas => decimal.Parse(_row[NamedColumnIndices.FireIceGas]);

		public decimal Deuterium => decimal.Parse(_row[NamedColumnIndices.Deuterium]);

		public bool IsTidallyLocked => Singularities.Any(a => a.EndsWith(NamedDescriptiveConstants.PerpetualTidalLock, StringComparison.InvariantCultureIgnoreCase));

		public string SingularitiesString => _row[NamedColumnIndices.SingularityFlags];
		public string[] Singularities => SingularitiesString.Split(",").Select(ss => ss.Trim('"')).ToArray();

		private readonly string[] _row;
		public PlanetInfo(string[] row)
		{
			_row = row;
		}

	}
}
