using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using ResponseAgent.Models;

namespace ResponseAgent.Services;

public class DiffService
{
    public List<DiffSegment> Build(string before, string after)
    {
        var diffBuilder = new InlineDiffBuilder(new Differ());
        var diff = diffBuilder.BuildDiffModel(before, after);

        var result = new List<DiffSegment>();
        string lastRemoved = "";
        int id = 0;

        foreach (var line in diff.Lines)
        {
            if (line.Type == ChangeType.Deleted)
            {
                lastRemoved = line.Text;
            }
            else if (line.Type == ChangeType.Inserted)
            {
                result.Add(new DiffSegment
                {
                    Id = id++,
                    Text = line.Text,
                    IsAdded = true,
                    BeforeText = lastRemoved
                });
                lastRemoved = "";
            }
            else
            {
                result.Add(new DiffSegment
                {
                    Id = id++,
                    Text = line.Text,
                    IsAdded = false
                });
                lastRemoved = "";
            }
        }

        return result;
    }
}
