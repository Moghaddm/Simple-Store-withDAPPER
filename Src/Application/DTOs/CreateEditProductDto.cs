namespace Cart.Application.DTOs;

public class CreateEditProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public string Slug { get; set; }
    public List<Attachment> Attachments { get; set; }

    public class Attachment
    {
        public string Title { get; set; }
        public string FileName { get; set; }
        public string Alt { get; set; }
    }
}
