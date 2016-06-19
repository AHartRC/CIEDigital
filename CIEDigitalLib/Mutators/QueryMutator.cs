using System.Linq;

namespace CIEDigitalLib.Mutators
{
    public delegate IQueryable<TItem> QueryMutator<TItem, TSearch>(IQueryable<TItem> items, TSearch search);
}