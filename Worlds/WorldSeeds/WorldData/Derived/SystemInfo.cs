using System.Collections.Generic;
using System.Linq;

using WorldData.Defined;

namespace WorldData.Derived
{
    public class SystemInfo
	{
		public string Name { get; set; }

		public decimal Luminosity => _systemRows.Average(f => decimal.Parse(f[NamedColumnIndices.Luminosity]));

		public string StarType => _systemRows.First()[NamedColumnIndices.StarType];

		public decimal DistanceFromStart => _systemRows.Average(s => decimal.Parse(s[NamedColumnIndices.DistanceFromHome]));

		public bool HasSulfur => Planets.Any(p => p.HasSulfuricAcid);

		public string[] Singularities => Planets.SelectMany(p => p.Singularities).ToArray();

		public PlanetInfo[] Planets { get; set; }

		private readonly string[][] _systemRows;

		public Dictionary<string, int> SingularitiesMap => Singularities.GroupBy(s => s).ToDictionary(s => s.Key, v => v.Count());

		public PlanetInfo[] Tidals => Planets.Where(p => p.IsTidallyLocked).ToArray();

		public SystemInfo(string systemName, string[][] systemRows)
		{
			Name = systemName;
			_systemRows = systemRows;

			Planets = _systemRows.Select(f => new PlanetInfo(f)).ToArray();

			// Fill the sat count
			var parentGroups = Planets.Where(p => !string.IsNullOrWhiteSpace(p.Parent)).GroupBy(p => p.Parent);
			foreach (var parentGrp in parentGroups)
			{
				var planet = Planets.Single(p => p.Name == parentGrp.Key);
				planet.Moons = parentGrp.ToArray();
			}
		}
	}
}
