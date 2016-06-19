using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using CIEDigitalLib.Models.Binding;

namespace CIEDigitalLib.Extensions
{
    public static class IEnumerableExtensions
    {
        public static DataTable AsDataTable<T>(this IEnumerable<T> data)
        {
            var properties = TypeDescriptor.GetProperties(typeof (T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var item in data)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static bool WriteToDatabase<T>(this IEnumerable<T> data, string tableName, SqlConnection conn,
            SqlBulkCopyOptions options)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            var transaction = conn.BeginTransaction();

            using (var bulkCopy = new SqlBulkCopy(conn, options, transaction))
            {
                bulkCopy.BatchSize = 1000;
                bulkCopy.DestinationTableName = string.Format("dbo.{0}", tableName);
                try
                {
                    bulkCopy.WriteToServer(data.AsDataTable());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    transaction.Rollback();
                    conn.Close();
                    return false;
                }
            }

            transaction.Commit();
            conn.Close();
            return true;
        }

        public static int GetTeamID(this IEnumerable<Team> teams, string teamName)
        {
            if (teamName.IsNullOrWhitespace())
                throw new ArgumentNullException("teamName");

            if (teams == null || !teams.Any())
                throw new ArgumentNullException("teams");

            if (teams.Any(a => a.ShortName == teamName))
            {
                return teams.First(f => f.ShortName == teamName).ID;
            }
            if (teams.Any(a => a.Franchise == teamName))
            {
                return teams.First(f => f.Franchise == teamName).ID;
            }
            if (teams.Any(a => a.Name == teamName))
            {
                return teams.First(f => f.Name == teamName).ID;
            }

            throw new ArgumentException(string.Format("No team was found that matches {0}", teamName), "teamName");
        }

        public static int? TryGetTeamID(this IEnumerable<Team> teams, string teamName)
        {
            if (teamName.IsNullOrWhitespace())
                return null;

            if (teams == null ||
                !teams.Any(a => a.ShortName == teamName || a.Franchise == teamName || a.Name == teamName))
                return null;

            if (teams.Any(a => a.ShortName == teamName))
            {
                return teams.First(f => f.ShortName == teamName).ID;
            }

            if (teams.Any(a => a.Name == teamName))
            {
                return teams.First(f => f.Name == teamName).ID;
            }

            if (teams.Any(a => a.Franchise == teamName))
            {
                return teams.First(f => f.Franchise == teamName).ID;
            }

            throw new ArgumentException(string.Format("No team was found that matches {0}", teamName), "teamName");
        }

        public static int GetPositionID(this IEnumerable<PlayerPosition> positions, string name)
        {
            if (positions == null || !positions.Any(a => a.ShortName.Equals(name) || a.Name.Equals(name)))
                throw new ArgumentNullException("positions", "There are no positions in the database");

            if (name.IsNullOrWhitespace())
                throw new ArgumentNullException("name", "An invalid entry was detected for the name of the position.");

            if (positions.Any(a => a.ShortName.Equals(name)))
                return positions.First(f => f.ShortName.Equals(name)).ID;

            if (positions.Any(a => a.Name.Equals(name)))
                return positions.First(f => f.Name.Equals(name)).ID;

            throw new ArgumentNullException("name", string.Format("No player position matches {0}", name));
        }

        public static int? TryGetPositionID(this DbSet<PlayerPosition> positions, string name)
        {
            if (positions == null || !positions.Any(a => a.ShortName.Equals(name) || a.Name.Equals(name)))
                throw new ArgumentNullException("positions", "There are no positions in the database");

            if (name.IsNullOrWhitespace())
                throw new ArgumentNullException("name", "An invalid entry was detected for the name of the position.");

            if (positions.Any(a => a.ShortName.Equals(name)))
                return positions.First(f => f.ShortName.Equals(name)).ID;

            if (positions.Any(a => a.Name.Equals(name)))
                return positions.First(f => f.Name.Equals(name)).ID;

            return null;
        }
    }
}