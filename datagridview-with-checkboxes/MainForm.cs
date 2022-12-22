using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.Versioning;

namespace datagridview_with_checkboxes
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            dataGridView.AllowUserToAddRows= false;
            dataGridView.DataSource = Items;
            Items.Add(new Item { Name = "AZI" });
            Items.Add(new Item { Name = "FRAN" });
            dataGridView.Columns[nameof(Item.IsChecked)].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView.Columns[nameof(Item.Name)].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Items.ListChanged += onListChanged;
            dataGridView.CellContentClick += onCellContentClick;
        }

        private void onCellContentClick(object? sender, DataGridViewCellEventArgs e)
        {            
            if (dataGridView.CurrentCell is DataGridViewCheckBoxCell)
            {
                dataGridView.EndEdit();
            }
        }

        private void onListChanged(object? sender, ListChangedEventArgs e)
        {
            if(e.ListChangedType.Equals(ListChangedType.ItemChanged))
            {

            }
        }

        BindingList<Item> Items = new BindingList<Item>();
    }
    class Item : INotifyPropertyChanged
    {
        bool _isChecked = false;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (!Equals(_isChecked, value))
                {
                    _isChecked = value;
                    PropertyChanged?.Invoke(
                        this, 
                        new PropertyChangedEventArgs(nameof(IsChecked)));
                }
            }
        }
        public string Name { get; set; } = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}