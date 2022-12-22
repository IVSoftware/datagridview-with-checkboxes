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
            dataGridView.Columns[nameof(Item.Selected)].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView.Columns[nameof(Item.Name)].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView.CellContentClick += onCellContentClick;
        }

        private void onCellContentClick(object? sender, DataGridViewCellEventArgs e)
        {            
            if (dataGridView.CurrentCell is DataGridViewCheckBoxCell)
            {
                dataGridView.EndEdit();

                Item item = Items[e.RowIndex];
                textBoxLast.Text = item.Selected ? item.Name : string.Empty;
                textBoxAll.Text =
                    string.Join(
                        ",",
                        Items.Where(_=>_.Selected).Select(_ => _.Name));
            }
        }
        BindingList<Item> Items = new BindingList<Item>();
    }
    class Item : INotifyPropertyChanged
    {
        public bool Selected { get; set; }
        public string Name { get; set; } = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}