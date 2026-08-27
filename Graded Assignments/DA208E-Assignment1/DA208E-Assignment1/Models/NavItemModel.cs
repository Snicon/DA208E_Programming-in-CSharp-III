/// <summary>
/// This class acts as a model for the NavItem components, which consists of a page and a label (data-wise).
/// </summary>
public class NavItemModel
{
    #region Fields
    private string _page;
    private string _label;
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