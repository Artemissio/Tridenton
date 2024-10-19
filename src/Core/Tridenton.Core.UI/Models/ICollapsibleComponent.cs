namespace Tridenton.Core.UI;

public interface ICollapsibleComponent
{
    bool Collapsed { get; set; }

    void Collapse()
    {
        Collapsed = !Collapsed;
    }
}