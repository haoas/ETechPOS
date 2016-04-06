using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms.Design;

namespace ETech.Views.Generic_Controls
{
    public class ControlGrid : UserControl
    {
        // Fields
        private Size _controlSize;
        private Type _controlType;
        public int _currentPage;
        private Size _gridDimensions;
        private Size _gridSpacing;
        private Size _initialGridSpacing;
        private DataTable _dataSource;
        private bool _showPagingButtons;
        private bool _showPageNumber;
        private bool _showFirstAndLast;

        private Control[] _controlArray;
        public Button _firstButton;
        private Button _lastButton;
        public Button _nextButton;
        private Button _previousButton;
        public TextBox _pageTextBox;
        private Label _pageLabel;
        private DataTable _template;

        // Properties
        /// <summary>
        /// Contains the row and column count of the ControlGrid
        /// </summary>
        public Control[] ControlArray
        {
            get { return _controlArray; }
            private set { _controlArray = value; }
        }
        [Browsable(true)]
        [Editor(typeof(TestDesignProperty), typeof(UITypeEditor))]
        [DefaultValue(null)]
        public Type ControlType
        {
            get { return _controlType; }
            set
            {
                if (_controlType != value)
                {
                    _controlType = value;
                    OnControlTypeChanged(new EventArgs());
                }
            }
        }
        public DataTable DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;
                OnDataSourceChanged(new EventArgs());
            }
        }
        public Size ControlSize
        {
            get { return ControlArray[0].Size; }
        }
        public Size GridDimensions
        {
            get { return _gridDimensions; }
            set
            {
                if (_gridDimensions != value)
                {
                    _gridDimensions = value;
                    OnGridDimensionChanged(new EventArgs());
                }
            }
        }
        public Size GridSpacing
        {
            get { return _gridSpacing; }
            set
            {
                if (_gridSpacing != value)
                {
                    _gridSpacing = value;
                    OnGridSpacingChanged(new EventArgs());
                }
            }
        }
        public bool ShowPageNumber
        {
            get { return _showPageNumber; }
            set
            {
                if (_showPageNumber != value)
                {
                    _showPageNumber = value;
                    OnShowOptionsChanged(new EventArgs());
                }
            }
        }
        public bool ShowFirstAndLast
        {
            get { return _showFirstAndLast; }
            set
            {
                if (_showFirstAndLast != value)
                {
                    _showFirstAndLast = value;
                    OnShowOptionsChanged(new EventArgs());
                }
            }
        }
        public bool ShowPagingButtons
        {
            get { return _showPagingButtons; }
            set
            {
                if (_showPagingButtons != value)
                {
                    _showPagingButtons = value;
                    OnShowOptionsChanged(new EventArgs());
                }
            }
        }
        private DataTable Template
        {
            get { return _template; }
            set
            {
                if (!_template.Equals(value))
                {
                    _template = value;
                    OnTemplateChanged(new EventArgs());
                }
            }
        }

        // Events
        private EventHandlerList _initializeEventList;
        //private IContainer components;
        private static readonly object _initializeEventKey = new object();

        public event EventHandler DataSourceChanged;
        public event EventHandler GridDimensionChanged;
        public event EventHandler GridSpacingChanged;
        public event EventHandler PageChanged;
        public event EventHandler ShowOptionsChanged;
        public event EventHandler TemplateChanged;
        public event EventHandler ControlTypeChanged;
        public event EventHandler Initialize
        {
            add
            {
                _initializeEventList.AddHandler(_initializeEventKey, value);
                OnInitialize(new EventArgs());
            }
            remove
            {
                _initializeEventList.RemoveHandler(_initializeEventKey, value);
                OnInitialize(new EventArgs());
            }
        }

        // Constructor
        public ControlGrid()
        {
            ControlTypeChanged += (sender, e) => { };
            DataSourceChanged += (sender, e) => { };
            GridDimensionChanged += (sender, e) => { };
            GridSpacingChanged += (sender, e) => { };
            Resize += new EventHandler(This_Resize);
            ShowOptionsChanged += (sender, e) => { };
            TemplateChanged += (sender, e) => { };

            // Default Values
            _initializeEventList = new EventHandlerList();
            _gridDimensions = new Size(1, 1);
            _gridSpacing = new Size(5, 5);
            _showFirstAndLast = false;
            _showPageNumber = true;
            _showPagingButtons = true;
            _controlArray = new Control[GridDimensions.Height * GridDimensions.Width];

            _nextButton = new Button();
            _nextButton.Name = "Next";
            _nextButton.Font = new Font("Arial", 14.25f, FontStyle.Regular);
            _nextButton.Text = "Next";
            _nextButton.Tag = "Next";
            _nextButton.Click += (sender, e) =>
            {
                _currentPage++;
                DataUpdate();
            };
            Controls.Add(_nextButton);

            _previousButton = new Button();
            _previousButton.Name = "Previous";
            _previousButton.Font = new Font("Arial", 14.25f, FontStyle.Regular);
            _previousButton.Text = "Previous";
            _previousButton.Tag = "Previous";
            _previousButton.Click += (sender, e) =>
            {
                _currentPage--;
                DataUpdate();
            };
            Controls.Add(_previousButton);

            _firstButton = new Button();
            _firstButton.Name = "First";
            _firstButton.Font = new Font("Arial", 14.25f, FontStyle.Regular);
            _firstButton.Text = "First";
            _firstButton.Tag = "First";
            _firstButton.Click += (sender, e) =>
            {
                _currentPage = 0;
                DataUpdate();
            };
            Controls.Add(_firstButton);

            _lastButton = new Button();
            _lastButton.Name = "Last";
            _lastButton.Font = new Font("Arial", 14.25f, FontStyle.Regular);
            _lastButton.Text = "Last";
            _lastButton.Tag = "Last";
            _lastButton.Click += (sender, e) =>
            {
                _currentPage = (int)Math.Ceiling(((double)DataSource.Rows.Count / (GridDimensions.Width * GridDimensions.Height))) - 1;
                DataUpdate();
            };
            Controls.Add(_lastButton);

            _pageTextBox = new TextBox();
            _pageTextBox.Font = new Font("Arial", 14.25f, FontStyle.Regular);
            _pageTextBox.Text = "1";
            _pageTextBox.TextAlign = HorizontalAlignment.Right;
            _pageTextBox.KeyPress += new KeyPressEventHandler(_pageTextBox_KeyPress);
            Controls.Add(_pageTextBox);

            _pageLabel = new Label();
            _pageLabel.Font = new Font("Arial", 14.25f, FontStyle.Regular);
            _pageLabel.Text = "/1";
            Controls.Add(_pageLabel);
        }

        public void _pageTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                _currentPage = int.Parse(_pageTextBox.Text) - 1;
                DataUpdate();
                _pageTextBox.Text = (_currentPage + 1).ToString();
            }
        }

        private void AdjustControlButtons()
        {
            // Show Options
            int horizontalSpaceToWorkWith = (GridSpacing.Width + _controlSize.Width) * GridDimensions.Width;
            Size buttonSize = _controlSize;
            int number, nextButtonSequenceId, previousButtonSequenceId, lastButtonSequenceId, firstButtonSequenceId, textBoxSequenceId;

            if (_showPagingButtons)
            {
                if (_showFirstAndLast)
                {
                    number = 4;
                    firstButtonSequenceId = 0; previousButtonSequenceId = 1; textBoxSequenceId = 2; nextButtonSequenceId = 2; lastButtonSequenceId = 3;
                    _firstButton.Show(); _previousButton.Show(); _nextButton.Show(); _lastButton.Show();
                }
                else
                {
                    number = 2;
                    firstButtonSequenceId = 0; previousButtonSequenceId = 0; textBoxSequenceId = 1; nextButtonSequenceId = 1; lastButtonSequenceId = 0;
                    _firstButton.Hide(); _previousButton.Show(); _nextButton.Show(); _lastButton.Hide();
                }
                if (_showPageNumber)
                {
                    number++;
                    nextButtonSequenceId++; lastButtonSequenceId++;
                    _pageTextBox.Show();
                    _pageLabel.Show();
                }
                else
                {
                    _pageTextBox.Hide();
                    _pageLabel.Hide();
                }
                buttonSize.Width = (horizontalSpaceToWorkWith - number * GridSpacing.Width) / number;

                _pageTextBox.Size = new Size(buttonSize.Width / 2, buttonSize.Height);
                _pageLabel.Size = new Size(buttonSize.Width / 2, buttonSize.Height);

                int temp = (buttonSize.Height - _pageTextBox.Height) / 2;
                _pageTextBox.Location = new Point(_initialGridSpacing.Width + textBoxSequenceId * horizontalSpaceToWorkWith / number,
                    _initialGridSpacing.Height + temp + (_controlSize.Height + GridSpacing.Height) * (GridDimensions.Height));

                // Found out that for TextBox.Text and Label.Text to be aligned vertically, Label.Location.Y + 3 = TextBox.Location.Y; 
                temp += 3;
                _pageLabel.Location = new Point(_initialGridSpacing.Width + buttonSize.Width / 2 + textBoxSequenceId * horizontalSpaceToWorkWith / number,
                    _initialGridSpacing.Height + temp + (_controlSize.Height + GridSpacing.Height) * (GridDimensions.Height));

                _firstButton.Size = buttonSize;
                _firstButton.Location = new Point(_initialGridSpacing.Width + firstButtonSequenceId * horizontalSpaceToWorkWith / number,
                    _initialGridSpacing.Height + (_controlSize.Height + GridSpacing.Height) * (GridDimensions.Height));

                _previousButton.Size = buttonSize;
                _previousButton.Location = new Point(_initialGridSpacing.Width + previousButtonSequenceId * horizontalSpaceToWorkWith / number,
                    _initialGridSpacing.Height + (_controlSize.Height + GridSpacing.Height) * (GridDimensions.Height));

                _nextButton.Size = buttonSize;
                _nextButton.Location = new Point(_initialGridSpacing.Width + nextButtonSequenceId * horizontalSpaceToWorkWith / number,
                    _initialGridSpacing.Height + (_controlSize.Height + GridSpacing.Height) * (GridDimensions.Height));

                _lastButton.Size = buttonSize;
                _lastButton.Location = new Point(_initialGridSpacing.Width + lastButtonSequenceId * horizontalSpaceToWorkWith / number,
                    _initialGridSpacing.Height + (_controlSize.Height + GridSpacing.Height) * (GridDimensions.Height));
            }
            else
            {
                _pageTextBox.Hide();
                _pageLabel.Hide();
                _firstButton.Hide();
                _previousButton.Hide();
                _nextButton.Hide();
                _lastButton.Hide();
            }
        }
        private void DataUpdate()
        {
            if (ControlType == null)
                return;
            if (DataSource != null)
            {
                int maxPage = (int)Math.Ceiling(((double)DataSource.Rows.Count / (GridDimensions.Width * GridDimensions.Height))) - 1;
                if (maxPage < 0)
                    maxPage = 0;
                _pageLabel.Text = "/" + (maxPage + 1).ToString();
                if (_currentPage < 0)
                    _currentPage = 0;
                else if (_currentPage > maxPage)
                    _currentPage = maxPage;
                int k = 0;
                var rows = DataSource.AsEnumerable().Skip(GridDimensions.Width * GridDimensions.Height * _currentPage).Take(GridDimensions.Width * GridDimensions.Height);
                List<PropertyInfo> propertyList = ControlType.GetProperties().ToList();
                foreach (DataRow row in rows)
                {
                    foreach (DataColumn column in DataSource.Columns)
                    {
                        PropertyInfo property;
                        try
                        {
                            property = propertyList.Find(x => x.Name == column.ColumnName);
                            if (property != null)
                                property.SetValue(_controlArray[k], row[column.ColumnName], null);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    _controlArray[k].Name = (GridDimensions.Width * GridDimensions.Height * _currentPage + k).ToString();
                    _controlArray[k].Show();
                    k++;
                }
                for (; k < GridDimensions.Width * GridDimensions.Height; k++)
                {
                    _controlArray[k].Hide();
                }
            }
            _pageTextBox.Text = (_currentPage + 1).ToString();
        }
        private void DuplicatePropertiesFromTemplate()
        {
            if (ControlType == null)
                return;
            Controls.Clear();
            Controls.Add(_nextButton);
            Controls.Add(_previousButton);
            Controls.Add(_lastButton);
            Controls.Add(_firstButton);
            Controls.Add(_pageTextBox);
            Controls.Add(_pageLabel);

            for (int i = 0; i < ControlArray.Length; i++)
            {
                ControlArray[i] = (Control)Activator.CreateInstance(ControlType);
                Controls.Add(ControlArray[i]);
            }
            OnInitialize(new EventArgs());
            ResizeControls();
        }
        protected void OnControlTypeChanged(EventArgs eventArgs)
        {
            DuplicatePropertiesFromTemplate();
            ControlTypeChanged(this, eventArgs);
        }
        protected void OnDataSourceChanged(EventArgs eventArgs)
        {
            DataUpdate();
            DataSourceChanged(this, eventArgs);
        }
        protected void OnGridDimensionChanged(EventArgs eventArgs)
        {
            ControlArray = new Control[GridDimensions.Height * GridDimensions.Width];
            DuplicatePropertiesFromTemplate();
            GridDimensionChanged(this, eventArgs);
        }
        protected void OnGridSpacingChanged(EventArgs eventArgs)
        {
            ResizeControls();
            GridSpacingChanged(this, eventArgs);
        }
        protected void OnPageChanged(EventArgs eventArgs)
        {
            PageChanged(this, eventArgs);
        }
        protected void OnInitialize(EventArgs eventArgs)
        {
            EventHandler initializeEventDelegate = (EventHandler)_initializeEventList[_initializeEventKey];
            if (initializeEventDelegate == null)
                return;
            initializeEventDelegate(this, eventArgs);
        }
        protected void OnShowOptionsChanged(EventArgs eventArgs)
        {
            ResizeControls();
            AdjustControlButtons();
            ShowOptionsChanged(this, eventArgs);
        }
        protected void OnTemplateChanged(EventArgs eventArgs)
        {
            DuplicatePropertiesFromTemplate();
            TemplateChanged(this, eventArgs);
        }
        private void ResizeControls()
        {
            if (ControlType != null)
            {
                _controlSize.Width = (Width - (GridSpacing.Width * GridDimensions.Width)) / GridDimensions.Width;
                _initialGridSpacing.Width = (Width - ((_controlSize.Width + GridSpacing.Width) * (GridDimensions.Width) - GridSpacing.Width)) / 2;

                int x = (_controlSize.Width * GridDimensions.Width) + (GridSpacing.Width * (GridDimensions.Width - 1));

                if (ShowPagingButtons)
                {
                    _controlSize.Height = (Height - (GridSpacing.Height * (GridDimensions.Height + 1))) / (GridDimensions.Height + 1);
                    _initialGridSpacing.Height = (Height - (_controlSize.Height + GridSpacing.Height) * (GridDimensions.Height + 1) + GridSpacing.Height) / 2;
                }
                else
                {
                    _controlSize.Height = (Height - (GridSpacing.Height * (GridDimensions.Height))) / GridDimensions.Height;
                    _initialGridSpacing.Height = (Height - (_controlSize.Height + GridSpacing.Height) * (GridDimensions.Height) + GridSpacing.Height) / 2;
                }
                int k = 0;
                if (ControlArray != null)
                {
                    if (ControlArray.Length == GridDimensions.Height * GridDimensions.Width)
                    {
                        for (int i = 0; i < GridDimensions.Height; i++)
                            for (int j = 0; j < GridDimensions.Width; j++)
                            {
                                if (ControlArray[k] != null)
                                {
                                    ControlArray[k].Size = _controlSize;
                                    ControlArray[k].Location = new Point(_initialGridSpacing.Width + (_controlSize.Width + GridSpacing.Width) * j,
                                        _initialGridSpacing.Height + (_controlSize.Height + GridSpacing.Height) * i);
                                }
                                k++;
                            }
                    }
                }
            }
            AdjustControlButtons();
        }
        private void This_Resize(object sender, EventArgs e)
        {
            ResizeControls();
        }

        private class TestDesignProperty : UITypeEditor
        {
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.DropDown;
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                var edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                ListBox lb = new ListBox();
                Type formType = typeof(Form);
                Type controlType = typeof(Control);
                foreach (var type in this.GetType().Assembly.GetTypes())
                {
                    if (type.IsSubclassOf(controlType) && !type.IsSubclassOf(formType))
                        lb.Items.Add(type);
                }
                foreach (var type in Assembly.Load("System.Windows.Forms").GetTypes())
                {
                    if (type.IsSubclassOf(controlType) && !type.IsSubclassOf(formType))
                        lb.Items.Add(type);
                }
                lb.Sorted = true;
                if (value != null)
                {
                    lb.SelectedItem = value;
                }

                edSvc.DropDownControl(lb);

                value = (Type)lb.SelectedItem;

                return value;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ControlGrid
            // 
            this.Name = "ControlGrid";
            this.ResumeLayout(false);

        }
    }
}
