using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using CIEDigitalLib.Attributes;
using CIEDigitalLib.Enumerators;
using CIEDigitalLib.Extensions;
using CIEDigitalLib.Models.Binding;
using CIEDigitalLib.Models.Context;

namespace CIEDigitalLib.Importers
{
    [CIEDigitalDatabase]
    public class NFLDataImporter
    {
        private CIEDigitalEntities db = new CIEDigitalEntities();

        public void ProcessDirectory(string directoryPath, NFLDataType dataType)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentNullException("directoryPath",
                    "The directory path must not be null, empty, or whitespace.");

            var di = new DirectoryInfo(directoryPath);

            // Directory may not exist if file path given
            if (!di.Exists)
            {
                // Grab the file information
                var fi = new FileInfo(directoryPath);

                // If the directory for the file information is not null, use it as our directory instead
                if (fi.Directory != null)
                    di = fi.Directory;
            }

            // Check again to see if the directory exists. It should be just the directory at this point so throw an exception if it doesnt.
            if (!di.Exists)
                throw new ArgumentNullException("directoryPath", "The directory you specified does not exist.");

            IEnumerable<FileInfo> files = di.EnumerateFiles("*.csv", SearchOption.AllDirectories).ToArray();

            if (!files.Any())
                throw new FileNotFoundException("There are no files in the specified directory");

            switch (dataType)
            {
                case NFLDataType.Organizations:
                    ProcessOrganizationFiles(files);
                    break;
                case NFLDataType.Teams:
                    ProcessTeamFiles(files);
                    break;
                case NFLDataType.Combine:
                    ProcessCombineFiles(files);
                    break;
                case NFLDataType.Weather:
                    ProcessWeatherFiles(files);
                    break;
                case NFLDataType.Results:
                    ProcessGameResultFiles(files);
                    break;
                default:
                    throw new ArgumentException(string.Format("Unknown NFL Data Type of {0}!", dataType));
            }
        }

        private void ProcessOrganizationFiles(IEnumerable<FileInfo> files)
        {
            Console.WriteLine("Processing Organization Files.");
            db = new CIEDigitalEntities();

            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file.FullName).ToList();

                var header = lines.First();

                lines.RemoveAt(0);

                var headers = header.SplitCSV().ToArray();
                var columnCount = headers.Length;

                foreach (var line in lines)
                {
                    var columns = line.SplitCSV().ToArray();

                    if (columns.All(a => a.IsNullOrWhitespace()))
                        continue;

                    var organization = new Organization
                    {
                        ShortName = columns[1],
                        FullName = columns[2]
                    };

                    db.Organizations.Add(organization);
                }

                //var organizations = (from line in lines
                //                     select line.SplitCSV().ToArray()
                //                         into columns
                //                         where !columns.All(a => a.IsNullOrWhitespace())
                //                         select new Organization()
                //                         {
                //                             ShortName = columns[1],
                //                             FullName = columns[2]
                //                         }).ToArray();

                //db.Organizations.AddRange(organizations);
            }

            db.SaveChanges();

            foreach (var organization in db.Organizations)
            {
                Console.WriteLine("{0} | {1} | {2}", organization.ID, organization.ShortName, organization.FullName);
            }

            Console.WriteLine("{0} organizations detected.", db.Organizations.Count());
        }

        private void ProcessTeamFiles(IEnumerable<FileInfo> files)
        {
            Console.WriteLine("Processing Team Files.");
            db = new CIEDigitalEntities();

            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file.FullName).ToList();

                var header = lines.First();

                lines.RemoveAt(0);

                var headers = header.SplitCSV().ToArray();
                var columnCount = headers.Length;

                db.Teams.AddRange(from line in lines
                    select line.SplitCSV().ToArray()
                    into columns
                    where !columns.All(a => a.IsNullOrWhitespace())
                    select new Team
                    {
                        ID = int.Parse(columns[0]),
                        Location = columns[2],
                        Franchise = columns[3],
                        Name = columns[1],
                        ShortName = columns[4]
                    });
            }

            db.SaveChanges();

            foreach (var team in db.Teams)
            {
                Console.WriteLine("{0} | {1} | {2} | {3} | {4}", team.ID, team.ShortName, team.Location, team.Franchise,
                    team.Name);
            }

            Console.WriteLine("{0} teams detected.", db.Teams.Count());
        }

        private void ProcessCombineFiles(IEnumerable<FileInfo> files)
        {
            Console.WriteLine("Processing Combine Files.");
            db = new CIEDigitalEntities();

            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file.FullName).ToList();

                var header = lines.First();

                lines.RemoveAt(0);

                var headers = header.SplitCSV().ToArray();
                var columnCount = headers.Length;

                db.Combines.AddOrUpdate((from line in lines
                    select line.SplitCSV().ToArray()
                    into columns
                    where !columns.All(a => a.IsNullOrWhitespace())
                    select new Combine
                    {
                        Year = int.Parse(columns[0]),
                        Name = columns[1],
                        FirstName = columns[2],
                        LastName = columns[3],
                        PositionID = columns[4].GetPositionID(),
                        HeightFeet = int.Parse(columns[5]),
                        HeightInches = decimal.Parse(columns[6]),
                        HeightInchesTotal = decimal.Parse(columns[7]),
                        WeightPounds = decimal.Parse(columns[8]),
                        Arms = decimal.Parse(columns[9]),
                        Hands = decimal.Parse(columns[10]),
                        FourtyYardDash = decimal.Parse(columns[11]),
                        TwentyYardDash = decimal.Parse(columns[12]),
                        TenYardDash = decimal.Parse(columns[13]),
                        TwentyYardShuttle = decimal.Parse(columns[14]),
                        ThreeCone = decimal.Parse(columns[15]),
                        Vertical = decimal.Parse(columns[16]),
                        Broad = int.Parse(columns[17]),
                        Bench = int.Parse(columns[18]),
                        Round = int.Parse(columns[19]),
                        College = columns[20],
                        Pick = columns[21],
                        PickRound = int.Parse(columns[22]),
                        PickTotal = int.Parse(columns[23]),
                        Wonderlic = int.Parse(columns[24]),
                        NFLGrade = decimal.Parse(columns[25])
                    }).ToArray());
            }

            db.SaveChanges();

            Console.WriteLine("{0} combine results detected.", db.Combines.Count());
        }

        private void ProcessWeatherFiles(IEnumerable<FileInfo> files)
        {
            Console.WriteLine("Processing Weather Files.");
            db = new CIEDigitalEntities();

            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file.FullName).ToList();

                var header = lines.First();

                lines.RemoveAt(0);

                var headers = header.SplitCSV().ToArray();
                var columnCount = headers.Length;

                foreach (var weather in from line in lines
                    select line.SplitCSV().ToArray()
                    into columns
                    where !columns.All(a => a.IsNullOrWhitespace())
                    select new Weather
                    {
                        GameID = columns[0],
                        HomeTeamID = db.Teams.GetTeamID(columns[1]),
                        HomeScore = int.Parse(columns[2]),
                        AwayTeamID = db.Teams.GetTeamID(columns[3]),
                        AwayScore = int.Parse(columns[4]),
                        Temperature = decimal.Parse(columns[5]),
                        WindChill = columns[6].ToNullableDecimal(),
                        Humidity =
                            columns[7].Trim('%').ToNullableDecimal() > 0
                                ? columns[7].Trim('%').ToNullableDecimal()/100m
                                : null,
                        WindMPH = columns[8].ToNullableDecimal(),
                        Description = columns[9],
                        Date = DateTime.Parse(columns[10])
                    })
                {
                    db.Weathers.Add(weather);
                }
            }

            db.SaveChanges();

            Console.WriteLine("{0} weather records detected.", db.Weathers.Count());
        }

        private void ProcessGameResultFiles(IEnumerable<FileInfo> files)
        {
            Console.WriteLine("Processing Result Files.");
            db = new CIEDigitalEntities();

            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file.FullName).ToList();

                var header = lines.First();

                lines.RemoveAt(0);

                var headers = header.SplitCSV().ToArray();
                var columnCount = headers.Length;

                foreach (var result in from line in lines
                    select line.SplitCSV().ToArray()
                    into columns
                    where !columns.All(a => a.IsNullOrWhitespace())
                    select new GameResult
                    {
                        Season = int.Parse(columns[0]),
                        Week = int.Parse(columns[1]),
                        KickOff = DateTime.Parse(columns[2]).ToUniversalTime(),
                        HomeTeamID = db.Teams.GetTeamID(columns[3]),
                        HomeScore = int.Parse(columns[4]),
                        AwayScore = int.Parse(columns[5]),
                        AwayTeamID = db.Teams.GetTeamID(columns[6])
                    })
                {
                    db.GameResults.Add(result);
                }
            }

            db.SaveChanges();

            Console.WriteLine("{0} result records detected.", db.GameResults.Count());
        }
    }
}