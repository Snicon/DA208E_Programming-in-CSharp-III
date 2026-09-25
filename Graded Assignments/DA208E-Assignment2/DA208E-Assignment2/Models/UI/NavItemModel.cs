// Sixten Peterson (AQ9300) 2026-09-14
/// <summary>
/// This class acts as a model for the NavItem components, which consists of a page and a label.
/// </summary>
public class NavItemModel
{
    #region Fields
    private string _page; // The page associated with the nav item
    private string _label; // A label for said page, this is what is shown as the text in the html
    #endregion
    
    #region Constructor
    public NavItemModel(string page, string label)
    {
        Page = page;
        Label = label;
    }
    #endregion
    
    #region Properties
    public string Page
    {
        get => _page;
        init
        {
            if (value.Trim() != String.Empty)
                _page = value.Trim();
        }
    }

    public string Label
    {
        get => _label;
        init
        {
            if (value.Trim() != String.Empty)
                _label = value.Trim();
        }
    }
    #endregion
}