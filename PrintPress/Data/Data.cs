namespace PrintPress.Data
{
    public abstract class Data
    {
        public event EventHandler<EventArgs>? DataChanged;
        protected virtual void OnDataChanged()
        {
            DataChanged?.Invoke(this, new EventArgs());
        }
        protected virtual void OnDataChanged(object? sender, EventArgs e)
        {
            DataChanged?.Invoke(sender, e);
        }
    }
}