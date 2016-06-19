using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Web;
using CIEDigitalLib.Enumerators;
using CIEDigitalLib.Helpers;
using CIEDigitalLib.Importers;
using CIEDigitalLib.Models.Binding;
using CIEDigitalLib.Models.Context;

namespace CIEDigitalLib.Initializers
{
    public sealed class CIEDigitalDatabaseInitializer : CreateDatabaseIfNotExists<CIEDigitalEntities>
        //public sealed class CIEDigitalDatabaseInitializer : DropCreateDatabaseAlways<CIEDigitalEntities>
    {
        public CIEDigitalDatabaseInitializer()
        {
            Database.SetInitializer<CIEDigitalEntities>(null);

            try
            {
                using (var db = new CIEDigitalEntities())
                {
                    if (!db.Database.Exists())
                    {
                        // Create the database without Entity Framework migration schema
                        ((IObjectContextAdapter) db).ObjectContext.CreateDatabase();
                        Seed(db);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The database could not be initialized!", ex);
            }
        }

        protected override void Seed(CIEDigitalEntities context)
        {
            DbContextHelper.AddElmahFunctionality(context);

            var positions = new[]
            {
                new PlayerPosition {Name = "Quarterback", ShortName = "QB"},
                new PlayerPosition {Name = "Running Back", ShortName = "RB"},
                new PlayerPosition {Name = "FullBack", ShortName = "FB"},
                new PlayerPosition {Name = "Wide Receiver", ShortName = "WR"},
                new PlayerPosition {Name = "Tight End", ShortName = "TE"},
                new PlayerPosition {Name = "Offensive Lineman", ShortName = "OL"},
                new PlayerPosition {Name = "Offensive Guard", ShortName = "OG"},
                new PlayerPosition {Name = "Offensive Tackle", ShortName = "OT"},
                new PlayerPosition {Name = "Center", ShortName = "C"},
                new PlayerPosition {Name = "Offensive Center", ShortName = "OC"},
                new PlayerPosition {Name = "Guard", ShortName = "G"},
                new PlayerPosition {Name = "Left Guard", ShortName = "LG"},
                new PlayerPosition {Name = "Right Guard", ShortName = "RG"},
                new PlayerPosition {Name = "Tackle", ShortName = "T"},
                new PlayerPosition {Name = "Left Tackle", ShortName = "LT"},
                new PlayerPosition {Name = "Right Tackle", ShortName = "RT"},
                new PlayerPosition {Name = "Kicker", ShortName = "K"},
                new PlayerPosition {Name = "Kick Returner", ShortName = "KR"},
                new PlayerPosition {Name = "Defensive Lineman", ShortName = "DL"},
                new PlayerPosition {Name = "Defensive End", ShortName = "DE"},
                new PlayerPosition {Name = "Defensive Tackle", ShortName = "DT"},
                new PlayerPosition {Name = "Nose Tackle", ShortName = "NT"},
                new PlayerPosition {Name = "Linebacker", ShortName = "LB"},
                new PlayerPosition {Name = "Inside Linebacker", ShortName = "ILB"},
                new PlayerPosition {Name = "Outside Linebacker", ShortName = "OLB"},
                new PlayerPosition {Name = "Middle Linebacker", ShortName = "MLB"},
                new PlayerPosition {Name = "Defensive Back", ShortName = "DB"},
                new PlayerPosition {Name = "CornerBack", ShortName = "CB"},
                new PlayerPosition {Name = "Free Safety", ShortName = "FS"},
                new PlayerPosition {Name = "Strong Safety", ShortName = "SS"},
                new PlayerPosition {Name = "Safety", ShortName = "S"},
                new PlayerPosition {Name = "Punter", ShortName = "P"},
                new PlayerPosition {Name = "Punt Returner", ShortName = "PR"},
                new PlayerPosition {Name = "Long Snapper", ShortName = "LS"},
                new PlayerPosition {Name = "Special Teams", ShortName = "ST"}
            };

            context.PlayerPositions.AddOrUpdate(positions);
            context.SaveChanges();
            var basePath = HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/Data/");
            NFLDataImporter importer = new NFLDataImporter();
            importer.ProcessDirectory(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/Data/Organizations"), NFLDataType.Organizations);
            importer.ProcessDirectory(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/Data/Teams"), NFLDataType.Teams);
            importer.ProcessDirectory(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/Data/Weather"), NFLDataType.Weather);
            importer.ProcessDirectory(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/Data/Results"), NFLDataType.Results);
            importer.ProcessDirectory(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/Data/Combine"), NFLDataType.Combine);
            base.Seed(context);
        }
    }
}