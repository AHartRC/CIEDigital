//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CIEDigitalLib.Models.Binding;
//using CIEDigitalLib.Mutators;

//namespace CIEDigitalLib.Filters
//{
//    public class GenericFilters<TItem, TSearch>
//    {
//        public List<SearchFieldMutator<Team, object>> TeamSearchFieldMutators = new List<SearchFieldMutator<Team, object>>()
//{
//    new SearchFieldMutator<Team, Func<TSearch>>(search => !string.IsNullOrWhiteSpace(search.Name), (users, search) => users.Where(u => u..Contains(search.Name) || u.LastName.Contains(search.Name))),
//    new SearchFieldMutator<Team, object>(search => !string.IsNullOrWhiteSpace(search.Email), (users, search) => users.Where(u => u.Email.Contains(search.Email))),
//    new SearchFieldMutator<Team, object>(search => search.UsertypeId.HasValue, (users, search) => users.Where(u => u.UsertypeId == search.UsertypeId.Value)),
//    new SearchFieldMutator<Team, object>(search => search.CurrentSort.ToLower() == "namedesc", (users, search) => users.OrderByDescending(u => u.FirstName).ThenByDescending(u => u.LastName)),
//    new SearchFieldMutator<Team, object>(search => search.CurrentSort.ToLower() == "emailasc", (users, search) => users.OrderBy(u => u.Email)),
//};
//    }
//}

