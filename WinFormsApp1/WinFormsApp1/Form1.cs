using Newtonsoft.Json;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private const string ApiEndpoint = "https://jsonplaceholder.typicode.com/posts";
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var posts = await FetchPostsAsync();

                var filteredPosts = FilterPosts(posts, 1, 10, 100);

                var jsonContent = JsonConvert.SerializeObject(filteredPosts, Formatting.Indented);

                WriteToJsonFile(jsonContent);

                MessageBox.Show("Məlumat test.txt faylına yazıldı!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Məlumatın yazılması zamanı xəta baş verdi: {ex.Message}");
            }
        }
        private async Task<Post[]> FetchPostsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(ApiEndpoint);
                return JsonConvert.DeserializeObject<Post[]>(response);
            }
        }

        private Post[] FilterPosts(Post[] posts, params int[] ids)
        {
            return Array.FindAll(posts, post => Array.Exists(ids, id => post.Id == id));
        }

        private void WriteToJsonFile(string jsonContent)
        {
            File.WriteAllText("test.txt", jsonContent);
        }
    }

    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

}