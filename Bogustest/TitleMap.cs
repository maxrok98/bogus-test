using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogustest {
  internal class TitleMap : ClassMap<Titles> {
    public TitleMap() {
      Map(m => m.Id).Index(0).Name("id");
      Map(m => m.Title).Index(1).Name("title");
      Map(m => m.Description).Index(2).Name("description");
      Map(m => m.ReleaseYear).Index(3).Name("release_year");
      Map(m => m.AgeCertification).Index(4).Name("age_certification");
      Map(m => m.Runtime).Index(5).Name("runtime");
      Map(m => m.Genres).Index(6).Name("genres");
      Map(m => m.ProductionCountry).Index(7).Name("production_country");
      Map(m => m.Seasons).Index(8).Name("seasons");
    }
  }
}
