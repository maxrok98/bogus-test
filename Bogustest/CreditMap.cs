using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogustest {
  internal class CreditMap : ClassMap<Credit> {
    public CreditMap() {
      Map(c => c.Id).Name("id");
      Map(c => c.TitleId).Name("title_id");
      Map(c => c.RealName).Name("real_name");
      Map(c => c.CharacterName).Name("character_name");
      Map(c => c.Role).Name("role");
    }
  }
}
