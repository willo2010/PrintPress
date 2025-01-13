using PrintPress.Data;
using PrintPress.Data.Enum;
using PrintPress.UICommand.ContentCommand;
using PrintPress.UICommand.ContentCommand.Journalism;
using PrintPress.UIService;

namespace PrintPress.UI
{
#if DEBUG
    internal partial class Journalism : PrintPress.UI.Tools.DummyContent
#else
    internal partial class Journalism : PrintPress.UI.ContentWindow
#endif
    {
        public JournalismService Service { get; init; }
        public override Department Type { get { return Service.Type; } }
        public Journalism(JournalismService journalismService) : base()
        {
            Service = journalismService;

            Service.ActiveChanging += ContentUpdating;
            Service.ActiveChanged += ContentUpdated;
            Service.ContentListChanged += OnContentsListChanged;

            InitializeComponent();

            ContentUpdated(null, new EventArgs());
            UpdateDisplay();
        }

        private void OnContentsListChanged(object? sender, EventArgs e)
        {
            UpdateContentListDisplay(Service.ContentList.Values.ToList());
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (UpdatingDisplay) return;
            AddStoryCommand command = new(Service);
            command.Execute();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (UpdatingDisplay) return;
            SaveActiveStoryCommand command = new(Service);
            command.Execute();
        }

        protected override void titleText_TextChanged(object sender, EventArgs e)
        {
            if (UpdatingDisplay) return;
            UpdateTitleTextCommand<JournalismService> command = new(Service, titleText.Text);
            command.Execute();
        }

        protected override void contentText_TextChanged(object sender, EventArgs e)
        {
            if (UpdatingDisplay) return;
            UpdateContentTextCommand<JournalismService> command = new(Service, contentText.Text);
            command.Execute();
        }

        protected override void notesText_TextChanged(object sender, EventArgs e)
        {
            if (UpdatingDisplay) return;
            UpdateNotesTextCommand<JournalismService> command = new(Service, notesText.Text);
            command.Execute();
        }
        protected override void contentStatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UpdatingDisplay) return;
            UpdateContentStatusCommand<JournalismService> command = new(Service, contentStatusComboBox.SelectedIndex);
            command.Execute();
        }

        protected override void contentListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UpdatingDisplay) return;
            UpdateActiveContentCommand<JournalismService> command = new(Service, contentListView.SelectedItems);
            command.Execute();
        }

        private void sourcesText_TextChanged(object sender, EventArgs e)
        {
            if (UpdatingDisplay) return;
            UpdateSourcesCommand command = new(Service, sourcesText.Text);
            command.Execute();
        }

        protected void OnSaveStateChanged(object? sender, EventArgs e)
        {
            if (Service.ActiveContent == null) return;
            SetSavedStateLabel(Service.ActiveContent.LocalChanges);
        }

        protected void UpdateContentDisplay(StoryData? content)
        {
            if (content == null)
            {
                return;
            }
            base.UpdateContentDisplay(content);
            
            UpdatingDisplay = true;
            sourcesText.Text = content.Sources;
            UpdatingDisplay = false;
        }

        private void ContentUpdating(object? sender, EventArgs e)
        {
            // Unset OnActiveDataChanged event.
            if (Service.ActiveContent == null) return;
            Service.ActiveContent.SaveStateChanged -= OnSaveStateChanged;
        }

        private void ContentUpdated(object? sender, EventArgs e)
        {
            // Update display
            UpdateContentDisplay(Service.ActiveContent);
            if (Service.ActiveContent == null) return;
            SetSavedStateLabel(Service.ActiveContent.LocalChanges);

            // Set OnActiveDataChanged event.
            Service.ActiveContent.SaveStateChanged += OnSaveStateChanged;
        }

        private void UpdateDisplay()
        {
            UpdateContentListDisplay(Service.ContentList.Values.ToList());
            UpdateContentDisplay(Service.ActiveContent);
        }
    }
}