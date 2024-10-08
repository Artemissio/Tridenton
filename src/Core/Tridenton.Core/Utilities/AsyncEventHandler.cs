namespace Tridenton.Core.Utilities;

public delegate ValueTask AsyncEventHandler(EventArgs e);
public delegate ValueTask AsyncEventHandler<in TEventArgs>(TEventArgs e);