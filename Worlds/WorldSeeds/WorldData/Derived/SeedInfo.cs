using System.IO;
using System.Linq;
using System.Threading.Tasks;

using WorldData.Defined;

namespace WorldData.Derived
{
    public class SeedInfo
	{
		private readonly string[][] _dataRows;

		public string Filename { get; set; }

		public string Seed { get; set; }

		public SystemInfo Home { get; }
		public PlanetInfo HomePlanet => Home.Planets.Single(p => p.CrudeOil > 0);
		public PlanetInfo[] OtherHomePlanets => Home.Planets.Except(Home.Planets.Where(p => p.IsGiant || p.CrudeOil > 0)).ToArray();
		public SystemInfo[] OtherSystems { get; }

		public SystemInfo[] Within6Ly => OtherSystems.Where(os => os.DistanceFromStart <= 6).OrderBy(os => os.DistanceFromStart).ToArray();

		private SeedInfo(string filename, string seed, string[][] dataRows)
		{
			Filename = Path.GetFileNameWithoutExtension(filename);
			Seed = seed;

			_dataRows = dataRows;

			var allSystems = _dataRows.GroupBy(f => f[NamedColumnIndices.StarName]).Select(f => new SystemInfo(f.Key, f.ToArray())).ToArray();
			Home = allSystems.Single(s => s.DistanceFromStart == 0);
			OtherSystems = allSystems.Except(new[] { Home }).ToArray();
		}

		public static async Task<SeedInfo> Read(string seed, string filename)
		{
			return new SeedInfo(filename, seed, (await File.ReadAllLinesAsync(filename)).Skip(1).Select(f => f.Split(",")).ToArray());
		}
	}
}
