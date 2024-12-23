namespace DataExportTool
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
#if NET5_0_OR_GREATER
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
#endif

            ApplicationConfiguration.Initialize();
            Application.Run(new Main());

        }
    }
}