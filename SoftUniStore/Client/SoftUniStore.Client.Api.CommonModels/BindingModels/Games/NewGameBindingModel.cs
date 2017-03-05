namespace SoftUniStore.Client.Api.CommonModels.BindingModels.Games
{
    public class NewGameBindingModel
    {
        public int GameId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public string Trailer { get; set; }
    }
}