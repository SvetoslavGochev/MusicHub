namespace MusicHub.DataProcessor
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using ImportDtos;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data";

        private const string SuccessfullyImportedWriter 
            = "Imported {0}";

        private const string SuccessfullyImportedProducerWithPhone 
            = "Imported {0} with phone: {1} produces {2} albums";

        private const string SuccessfullyImportedProducerWithNoPhone
            = "Imported {0} with no phone number produces {1} albums";

        private const string SuccessfullyImportedSong 
            = "Imported {0} ({1} genre) with duration {2}";

        private const string SuccessfullyImportedPerformer
            = "Imported {0} ({1} songs)";

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            var writersDto = JsonConvert.DeserializeObject<ImportWriterDto[]>(jsonString);

            var sb = new StringBuilder();
            var validWriters = new List<Writer>();



            return null;
        }

        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {
            var producerDtos = JsonConvert.DeserializeObject<ImportProducerDto[]>(jsonString);

            var sb = new StringBuilder();
            var validProducers = new List<Producer>();


            return null;

        }

        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportSongDto[]), new XmlRootAttribute("Songs"));
            var songDtos = (ImportSongDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();
        
            return null;
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {
            var performerDtos = DeserializeObject<ImportPerformerDto>("Performers", xmlString);

            var validPerformers = new List<Performer>();
            var sb = new StringBuilder();

            return null;

        }

        private static T[] DeserializeObject<T>(string rootElement, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(T[]), new XmlRootAttribute(rootElement));
            var deserializedDtos = (T[])xmlSerializer.Deserialize(new StringReader(xmlString));
            return deserializedDtos;
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResult = new List<ValidationResult>();

            var result = Validator.TryValidateObject(entity, validationContext, validationResult, true);

            return result;
        }
    }
}