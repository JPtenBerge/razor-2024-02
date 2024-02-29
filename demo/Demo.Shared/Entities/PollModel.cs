using System.Collections;

namespace Demo.Shared.Entities;

public class PollModel
{
    public string Question { get; set; }

    public IEnumerable<OptionModel> Options { get; set; }
}