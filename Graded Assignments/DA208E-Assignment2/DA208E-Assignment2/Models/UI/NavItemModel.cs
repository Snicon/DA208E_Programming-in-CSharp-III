// Sixten Peterson (AQ9300) 2026-09-14
/// <summary>
/// This class acts as a model for the NavItem components, which consists of a page and a label.
/// </summary>
public class NavItemModel
{
    #region Fields
    private string _controller;
    private string _action;
    private string _label; // A label for said page, this is what is shown as the text in the html
    #endregion
    
    #region Constructor
    public NavItemModel(string controller, string action, string label)
    {
        Controller = controller;
        Action = action;
        Label = label;
    }
    #endregion
    
    #region Properties
    public string Controller
    {
        get => _controller;
        init
        {
            if (value.Trim() != String.Empty)
                _controller = value.Trim();
        }
    }
    
    public string Action
    {
        get => _action;
        init
        {
            if (value.Trim() != String.Empty)
                _action = value.Trim();
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