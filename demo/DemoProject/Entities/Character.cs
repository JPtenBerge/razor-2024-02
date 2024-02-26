namespace DemoProject.Entities;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsBender { get; set; }
    public List<string>? Elements { get; set; } = new();
    public string PhotoUrl { get; set; }
}