namespace MovieAPI.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Rating { get; set; }
        public string? Image { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
    }

    public class MovieAddViewModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public float Rating { get; set; }
        public string? Image { get; set; }
    }
}
