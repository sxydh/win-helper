using win_helper.Services;

namespace win_helper.Views
{
    public partial class MyButton : Button
    {

        public BaseService Service { get; set; }

        public MyButton(BaseService service)
        {
            Service = service;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
