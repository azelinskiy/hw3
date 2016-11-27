namespace WorldUniversityRankings.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Net;

    internal sealed class Configuration : DbMigrationsConfiguration<WorldUniversityRankings.Data.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WorldUniversityRankings.Data.Context context)
        {
            //  This method will be called after migrating to the latest version.

            int[] years = new int[] { 2016, 2015, 2014, 2013, 2012 };
            using (WebClient webClient = new WebClient())
            {
                string page;
                List<List<string>> table = new List<List<string>>();
                foreach (var year in years)
                {
                    page = webClient.DownloadString("http://cwur.org/" + year + ".php");

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(page);

                    table = doc.DocumentNode.SelectSingleNode("//table[@class='table table-bordered table-hover']")
                    .Descendants("tr")
                    .Where(tr => tr.Elements("td").Count() > 1)
                    .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                    .ToList();

                    string univ, count;
                    foreach (var row in table)
                    {

                        if (row.Count == 13)
                        {
                            count = row[2];
                            context.Locations.AddOrUpdate(l => l.Country, new Location { Country = count });
                            context.SaveChanges();
                            context.Institutions.AddOrUpdate(i => i.Name, new Institution { Name = row[1], Location = context.Locations.First(l => l.Country == count) });
                            context.SaveChanges();
                            univ = row[1];
                            context.Years.AddOrUpdate(new Year
                            {
                                year = year,
                                WorldRank = int.Parse(row[0]),
                                Institution = context.Institutions.First(i => i.Name == univ),
                                NationalRank = int.Parse(row[3]),
                                QualityOfEducation = int.Parse(row[4]),
                                AlumniEmployment = int.Parse(row[5]),
                                QualityOfFaculty = int.Parse(row[6]),
                                Publications = int.Parse(row[7]),
                                Influence = int.Parse(row[8]),
                                Citations = int.Parse(row[9]),
                                BroadImpact = int.Parse(row[10]),
                                Patents = int.Parse(row[11]),
                                Score = double.Parse(row[12].Replace('.', ','))
                            });
                            context.SaveChanges();
                        }
                        if (row.Count == 12)
                        {
                            count = row[2];
                            context.Locations.AddOrUpdate(l => l.Country, new Location { Country = count });
                            context.SaveChanges();
                            context.Institutions.AddOrUpdate(i => i.Name, new Institution { Name = row[1], Location = context.Locations.First(l => l.Country == count) });
                            context.SaveChanges();
                            univ = row[1];
                            context.Years.AddOrUpdate(y => new { y.WorldRank, y.year }, new Year
                            {
                                year = year,
                                WorldRank = int.Parse(row[0]),
                                Institution = context.Institutions.First(i => i.Name == univ),
                                NationalRank = int.Parse(row[3]),
                                QualityOfEducation = int.Parse(row[4]),
                                AlumniEmployment = int.Parse(row[5]),
                                QualityOfFaculty = int.Parse(row[6]),
                                Publications = int.Parse(row[7]),
                                Influence = int.Parse(row[8]),
                                Citations = int.Parse(row[9]),
                                Patents = int.Parse(row[10]),
                                Score = double.Parse(row[11].Replace('.', ','))
                            });
                            context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
