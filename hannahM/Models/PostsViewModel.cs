namespace hannahM.Models
{
    public class PostsViewModel
    {
        public Blog blogpost { get; set; }
        public RandomThoughts randompost { get; set; }        
        public List<Blog> listBlog { get; set; }
        public List<RandomThoughts> listRandom { get; set; }
    }
}
