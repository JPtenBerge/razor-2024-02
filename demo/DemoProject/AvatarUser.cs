using Microsoft.AspNetCore.Identity;

namespace DemoProject;

public class AvatarUser : IdentityUser
{
    public bool KanBenden { get; set; }
}