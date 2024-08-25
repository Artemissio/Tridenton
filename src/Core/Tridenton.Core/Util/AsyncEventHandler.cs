namespace Tridenton.Core.Util;

public delegate ValueTask AsyncEventHandler(EventArgs e);
public delegate ValueTask AsyncEventHandler<in TEventArgs>(TEventArgs e);