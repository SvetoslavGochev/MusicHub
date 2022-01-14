using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using MusicHub.DataProcessor.ExportDtos;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using System.Collections.Generic;

namespace MusicHub.DataProcessor
{
    using System;

    using Data;

    public class Serializer
    {
        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {

            var albums = context.Albums
                .Where(x => x.ProducerId == producerId)
                .OrderByDescending(x => x.Price)
                .Select(x => new ExportAlbumDto
                {
                    AlbumName = x.Name,
                    ProducerName = x.Producer.Name,
                    ReleaseDate = x.ReleaseDate.ToString(@"MM/dd/yyyy"),
                    Songs = x.Songs.Select(s => new ExportAlbumSongsDto
                    {
                        SongName = s.Name,
                        Price = s.Price.ToString("F2"),
                        Writer = s.Writer.Name
                    })
                    .OrderByDescending(s => s.SongName)
                    .ThenBy(s => s.Writer)
                    .ToList(),
                    AlbumPrice = x.Price.ToString("F2")
                    

                })
                .ToList();


            //var albums = context
            //    .Albums
            //    .Where(p => p.ProducerId == producerId)
            //    .Select(x => new
            //    {
            //        AlbumName = x.Name,
            //        ReleaseDate = x.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
            //        ProducerName = x.Producer.Name,
            //        Songs = x.Songs.Select(s => new
            //            {
            //                SongName = s.Name,
            //                Price = s.Price.ToString("F2"),
            //                Writer = s.Writer.Name
            //            })
            //            .OrderByDescending(n => n.SongName)
            //            .ThenBy(w => w.Writer)
            //            .ToArray(),
            //        AlbumPrice = x.Price.ToString("F2")
            //    })
            //    .OrderByDescending(p => decimal.Parse(p.AlbumPrice))
            //    .ToArray();

            return JsonConvert.SerializeObject(albums, Formatting.Indented);
   
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {


            var songs = context.Songs
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s => new ExportSongDto
                {
                    SongName = s.Name,
                    Writer = s.Writer.Name,
                    Performer = s.SongPerformers
                    .Select(p => p.Performer.FirstName + " " + p.Performer.LastName).FirstOrDefault(),
                    Duration = s.Duration.ToString("C")


                })
                .OrderBy(s => s.SongName)
                .ThenBy(s => s.Writer)
                .ThenBy(s => s.Performer)
                .ToList();


            var xmlSerializer = new XmlSerializer(typeof(ExportSongDto[]), new XmlRootAttribute("Songs"));

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[]
            {
                    XmlQualifiedName.Empty
                });
            xmlSerializer.Serialize(new StringWriter(sb), songs, namespaces);
            return sb.ToString().TrimEnd();
        }
    }
}