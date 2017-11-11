namespace FullSender
{
    static class Helpers
    {
        public const string ConnectionStringKey = "Microsoft.ServiceBus.ConnectionString";
        public const string SessionQueueName = "basic-session-queue";
        public const string BasicQueueName = "basic-queue";
        public const string BasicTopicName = "basic-topic";
        public const string ResponseQueue = "temp-response-queue";

        public static dynamic GetModels()
        {
            return new[]
           {
                new {name = "Einstein", firstName = "Albert"},
                new {name = "Heisenberg", firstName = "Werner"},
                new {name = "Curie", firstName = "Marie"},
                new {name = "Hawking", firstName = "Steven"},
                new {name = "Newton", firstName = "Isaac"},
                new {name = "Bohr", firstName = "Niels"},
                new {name = "Faraday", firstName = "Michael"},
                new {name = "Galilei", firstName = "Galileo"},
                new {name = "Kepler", firstName = "Johannes"},
                new {name = "Kopernikus", firstName = "Nikolaus"}
            };

        }
    }
}
